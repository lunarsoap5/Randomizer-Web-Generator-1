window.addEventListener("DOMContentLoaded", () => {
    const $ = window.$;

    let worker = new Worker("/js/patcher/app_worker.js");

    function base64ToBytes(base64) {
        const binString = atob(base64);
        return Uint8Array.from(binString, (m) => m.codePointAt(0));
    }
      

    let patching_status = (() => {
        let is_patching = false;
        return {
            get is_patching() { return is_patching; },
            set is_patching(value) {
                if (value) {
                    $("#patch_btn").attr("disabled", "disabled");
                    $("#patch_status").show();
                    $("#patch_error").hide();
                    $("#patch_progress").removeAttr("value");
                } else {
                    $("#patch_status").hide();
                    $("#patch_error").hide();
                    $("#patch_progress").removeAttr("value");
                    if ($("#iso_in")[0].files.length === 0) {
                        $("#patch_btn").attr("disabled", "disabled");
                    } else {
                        $("#patch_btn").removeAttr("disabled");
                    }
                }
                is_patching = value
            },
        }
    })();

    patching_status.is_patching = false;

    $("#iso_in").on("change", (evt) => {
        let file = evt.target.files[0];
        if (file) {
            if (patching_status.is_patching) {
                $("#patch_btn").attr("disabled", "disabled");
            } else {
                $("#patch_btn").removeAttr("disabled");
            }
        } else {
            $("#patch_btn").removeAttr("disabled");
        }
    });

    $("#patch_btn").on("click", (evt) => {
        if ($("#iso_in")[0].files.length === 0) {
            return;
        }

        patching_status.is_patching = true;

        // TODO: Check the region of the iso before fetching the patch
        window.tpr.shared.callCreateGci(window.tpr.shared.genFcSettingsString(true), (error, data) => {
            if (error) {
                console.log('error in response');
                console.log(error);
                $('#patch_error').text('Failed to get patch.').show();
                patching_status.is_patching = false;
            } else if (data) {
                console.log('success in response');
                console.log(data);
                $('#patch_error').hide();
                let {name, bytes} = data[0];
                console.log(name);
                let patchBytes = base64ToBytes(bytes);
                console.log(patchBytes);
                let patch = new Blob([patchBytes], { type: 'application/octet-stream' });
                let file = $("#iso_in")[0].files[0];
                worker.postMessage({ type: "run", file, patch });
            }
        });
    });

    function setupDownload(file, filename) {
        let a = document.createElement("a");
        a.style.display = "none";
        a.download = filename;
        let url = window.URL.createObjectURL(file);
        a.href = url;
        document.body.appendChild(a);
        a.click();
        console.debug("Download done. Cleaning...");
        document.body.removeChild(a);
        window.URL.revokeObjectURL(url);
    }

    async function downloadIso(filename) {
        let root = await navigator.storage.getDirectory();
        let fileHandle = await root.getFileHandle("out.iso");
        setupDownload(await fileHandle.getFile(), filename);
    }

    worker.addEventListener("message", (event) => {
        switch (event.data.type) {
            case "progress": {
                console.debug("Progress", event.data.title, event.data.progress);
                if (typeof event.data.progress !== "undefined") {
                    $("#patch_status_text").text(event.data.title);
                } else {
                    $("#patch_status_text").text("");
                }
                if (typeof event.data.progress == "number") {
                    $("#patch_progress").attr("value", event.data.progress);
                    $("#patch_progress_text").text(event.data.progress.toLocaleString(undefined, {maximumFractionDigits: 1, minimumFractionDigits: 1}) + "%");
                } else {
                    $("#patch_progress_text").text("");
                    $("#patch_progress").removeAttr("value");
                }
                if (typeof event.data.title !== "string" && typeof event.data.progress !== "number") {
                    $("#patch_status").hide();
                } else {
                    $("#patch_status").show();
                }
                break;
            }
            case "done": {
                patching_status.is_patching = false;
                console.debug("Done", event.data.filename);
                $("#patch_status_text").text("Done");
                downloadIso(event.data.filename);
                $("#patch_status").hide();
                break;
            }
            case "cancelled": {
                patching_status.is_patching = false;
                console.debug("Cancelled", event.data.msg);
                $("#patch_status_text").text("Cancelled");
                $("#patch_error").text(event.data.msg).show();
                break;
            }
        }
    });
});