var formChecker = null;
/*timer-checker*/

function swfUploadLoaded() {
    var btnSubmit = document.getElementById("btnSubmit");
    btnSubmit.onclick = doSubmit;
    btnSubmit.disabled = true;
    document.getElementById("resumeFileName").value = '';
    formChecker = window.setInterval(validateForm, 1000);

    validateForm();
}

function validateForm() {
    var isValid = true;

    var email = document.getElementById("resumeEmail");
    isValid = validateEmail(email.getAttribute("id"), false);

    var fileName = document.getElementById("resumeFileName");
    if (jQuery.trim(fileName.value) === "") {
        isValid = false;
    }

    document.getElementById("btnSubmit").disabled = !isValid;
}

//function validateEmail(emailId, onSubmit) {
//    return true;
//}
function validateEmail(emailId, onSubmit) { // bool onSubmit - shows that validation is called on submit button click
    var isValid = true;
    var setErrorClass = false;
    var email = document.getElementById(emailId);
    var regexp = /^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$/;

    if (jQuery.trim(email.value) === "") {
        isValid = false;
        setErrorClass = onSubmit;
    }
    else {
        isValid = regexp.test(email.value);
        setErrorClass = !isValid;
    }

    if (setErrorClass)
        email.setAttribute("class", "input-validation-error");
    else
        email.setAttribute("class", "");

    return isValid;
}



// Called by the submit button to start the upload

function doSubmit(e) {
    if (formChecker != null) {
        clearInterval(formChecker);
        formChecker = null;
    }

    e = e || window.event;
    if (e.stopPropagation) {
        e.stopPropagation();
    }
    e.cancelBubble = true;

    try {
        window.swfu.startUpload();
    } catch(ex) {

    }
    return false;
}


function fileDialogStart() {
    document.getElementById("resumeFileName").value = "";
    window.swfu.cancelUpload();
}


function fileQueueError(file, errorCode, message) {
    try {
        // Handle this error separately because we don't want to create a FileProgress element for it.
        switch (errorCode) {
        case SWFUpload.QUEUE_ERROR.QUEUE_LIMIT_EXCEEDED:
            alert("You have attempted to queue too many files.\n" + (message === 0 ? "You have reached the upload limit." : "You may select " + (message > 1 ? "up to " + message + " files." : "one file.")));
            return;
        case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
            alert("The file you selected is too big.");
            window.swfu.debug("Error Code: File too big, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
            return;
        case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
            alert("The file you selected is empty.  Please select another file.");
            window.swfu.debug("Error Code: Zero byte file, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
            return;
        case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
            alert("The file you choose is not an allowed file type.");
            window.swfu.debug("Error Code: Invalid File Type, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
            return;
        default:
            alert("An error occurred in the upload. Try again later.");
            window.swfu.debug("Error Code: " + errorCode + ", File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
            return;
        }
    } catch(e) {
    }
}

function fileQueued(file) {
    try {
        document.getElementById("resumeFileName").value = file.name;
    } catch(e) {
    }

}

function fileDialogComplete(numFilesSelected, numFilesQueued) {
    validateForm();
}

function uploadProgress(file, bytesLoaded, bytesTotal) {

    try {
        var percent = Math.ceil((bytesLoaded / bytesTotal) * 100);

        file.id = "singlefile"; // This makes it so FileProgress only makes a single UI element, instead of one for each file
        var progress = new FileProgress(file, window.swfu.customSettings.progress_target);
        progress.setProgress(percent);
        progress.setStatus("Uploading...");
    } catch(e) {
    }
}

function uploadSuccess(file, serverData) {
    try {
        file.id = "singlefile"; // This makes it so FileProgress only makes a single UI element, instead of one for each file
        var progress = new FileProgress(file, window.swfu.customSettings.progress_target);
        progress.setComplete();
        progress.setStatus("Complete.");
        progress.toggleCancel(false);

        if (jQuery.trim(serverData) === "") {
            window.swfu.customSettings.upload_successful = false;
        } else {
            handleServerData(file, serverData);
        }

    } catch(e) {
    }
}

function handleServerData(file, serverData) {
    var jsonServerData = JSON.parse(serverData);
    var resumeFileNameValmsg = document.getElementById("resumeFileNameValmsg");
    
    window.swfu.customSettings.upload_successful = jsonServerData.Result == 1;
    document.getElementById("resumeFileId").value = jsonServerData.FileId;
    if (jsonServerData.Result == 0) {
        resumeFileNameValmsg.textContent = jsonServerData.ErrorMessage;
        resumeFileNameValmsg.setAttribute("class", "field-validation-error");
    }else {
        resumeFileNameValmsg.textContent = "";
        resumeFileNameValmsg.setAttribute("class", "field-validation-valid");
    }
}

function uploadComplete(file) {
    try {
        if (window.swfu.customSettings.upload_successful) {
            uploadDone();
        } else {
            uploadError(file, SWFUpload.UPLOAD_ERROR.UPLOAD_FAILED);
            var fileName = document.getElementById("resumeFileName");
            fileName.value = "";
            validateForm();

            //alert("There was a problem with the upload.\nThe server did not accept it.");
        }

        swfUploadLoaded(); // re-init timer 
    } catch(e) {
    }
}

// Called by the queue complete handler to submit the form

function uploadDone() {
    if (window.swfu.customSettings.upload_successful) {
        try {
            sendForm();
        } catch(ex) {
            alert("Error submitting form");
        }
    }
}



function uploadError(file, errorCode, message) {
    try {

        if (errorCode === SWFUpload.UPLOAD_ERROR.FILE_CANCELLED) {
            // Don't show cancelled error boxes
            return;
        }

        var fileName = document.getElementById("resumeFileName");
        fileName.value = "";
        validateForm();

        // Handle this error separately because we don't want to create a FileProgress element for it.
        switch (errorCode) {
        case SWFUpload.UPLOAD_ERROR.MISSING_UPLOAD_URL:
            alert("There was a configuration error.  You will not be able to upload a resume at this time.");
            window.swfu.debug("Error Code: No backend file, File name: " + file.name + ", Message: " + message);
            return;
        case SWFUpload.UPLOAD_ERROR.UPLOAD_LIMIT_EXCEEDED:
            alert("You may only upload 1 file.");
            window.swfu.debug("Error Code: Upload Limit Exceeded, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
            return;
        case SWFUpload.UPLOAD_ERROR.HTTP_ERROR:
        case SWFUpload.UPLOAD_ERROR.UPLOAD_FAILED:
        case SWFUpload.UPLOAD_ERROR.IO_ERROR:
        case SWFUpload.UPLOAD_ERROR.SECURITY_ERROR:
        case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:
        case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:
            break;
        default:
            alert("An error occurred in the upload. Try again later.");
            window.swfu.debug("Error Code: " + errorCode + ", File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
            return;
        }


        file.id = "singlefile"; // This makes it so FileProgress only makes a single UI element, instead of one for each file
        var progress = new FileProgress(file, window.swfu.customSettings.progress_target);
        progress.setError();

        switch (errorCode) {
        case SWFUpload.UPLOAD_ERROR.HTTP_ERROR:
            progress.setStatus("Upload Error");
            window.swfu.debug("Error Code: HTTP Error, File name: " + file.name + ", Message: " + message);
            break;
        case SWFUpload.UPLOAD_ERROR.UPLOAD_FAILED:
            progress.setStatus("Upload Failed.");
            window.swfu.debug("Error Code: Upload Failed, File name: " + file.name + ", File size: " + file.size + ", Message: " + message);
            break;
        case SWFUpload.UPLOAD_ERROR.IO_ERROR:
            progress.setStatus("Server (IO) Error");
            window.swfu.debug("Error Code: IO Error, File name: " + file.name + ", Message: " + message);
            break;
        case SWFUpload.UPLOAD_ERROR.SECURITY_ERROR:
            progress.setStatus("Security Error");
            window.swfu.debug("Error Code: Security Error, File name: " + file.name + ", Message: " + message);
            break;
        case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:
            progress.setStatus("Upload Cancelled");
            window.swfu.debug("Error Code: Upload Cancelled, File name: " + file.name + ", Message: " + message);
            break;
        case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:
            progress.setStatus("Upload Stopped");
            window.swfu.debug("Error Code: Upload Stopped, File name: " + file.name + ", Message: " + message);
            break;
        }

        progress.toggleCancel(false);

    } catch(ex) {
    }
}