function ShowMessage(message, messagetype) {
    var cssclass;
    switch (messagetype) {
        case 'Success':
            cssclass = 'alert-dismissible alert-success'
            break;
        case 'Error':
            cssclass = 'alert-dismissible alert-danger'
            break;
        case 'Warning':
            cssclass = 'alert-dismissible alert-warning'
            break;
        default:
            cssclass = 'alert-dismissible alert-primary'
    }
    $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999; width: 80%;" class="alert ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
    $('#alert_container2').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999; width: 80%;" class="alert ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
}