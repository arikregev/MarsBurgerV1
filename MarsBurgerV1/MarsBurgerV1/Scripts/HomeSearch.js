    $(document).ready(function () {
        $("#target").keyup(function () {
            var query = $(this).val();
            getItems(query);
        });

    function getItems(query) {
        $.ajax({
            url: '@Url.Action("RemoteData", "Home")',
            data: { "query": query },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.Data != null) {
                    if ($("#targetUL") != undefined) {
                        $("#targetUL").remove();
                    }
                    data = response.Data;
                    $("#targetDiv").append($("<ul id='targetUL'></ul>"));
                    $("#targetUL").find("li").remove();
                    $.each(data, function (i, value) {
                        $("#targetUL").append($("<li class='targetLI' onclick='javascript:appendTextToTextBox(this)'>" + value + "</li>"));

                    });
                }
                else {
                    $("#targetUL").find("li").remove();
                    $("#targetUL").remove();
                }
            },
            error: function (xhr, status, error) {
            }
        });
    }
});
    function appendTextToTextBox(e) {
        var textToappend = e.innerText;
    $("#target").val(textToappend);
    $("#targetUL").remove();
}
