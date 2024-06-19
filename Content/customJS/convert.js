
function selectListItemToArray(obj) {
    var html = "";
    for (var i = 0; i < obj.length; i++) {
        html += "<option value='" + obj[i].Value + "'>" + obj[i].Text + "</option>";
    }  
    return html;
}