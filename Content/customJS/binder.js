function BindDLL(obj, id,url) {
    $("#" + id).html();
    $.post(url, { key: obj.value }, function (data) {
        console.log(data);
        if (data!=null) {
            $("#" + id).html(data);
        }
    }).fail(function () {alert("An Error Occured While Connecting To The Server") })
}