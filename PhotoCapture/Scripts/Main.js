$('.btn-xs').click(function () {
    if ($(this).hasClass('btn-primary')) {
        $(this).removeClass('btn-primary');
        $(this).addClass('btn-danger');
        $(this).find('span').removeClass('glyphicon-plus');
        $(this).find('span').addClass('glyphicon-minus');
    } else {
        $(this).removeClass('btn-danger');
        $(this).addClass('btn-primary');
        $(this).find('span').removeClass('glyphicon-minus');
        $(this).find('span').addClass('glyphicon-plus');
    }
    
});

$('#location').click(function () {
    if ($(this).hasClass('btn-success')) {
        $(this).removeClass('btn-success');
        $('#CurrentLocation').val("");
    }
    else {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition, error);
            function showPosition(position) {
                $('#CurrentLocation').val(position.coords.latitude +
                "," + position.coords.longitude);
                $('#location').addClass('btn-success');
            };
            function error(err) {
                console.warn('ERROR(' + err.code + '): ' + err.message);
                $('#warning').text('ERROR(' + err.code + '): ' + err.message);
                
            };
            
        } else {
            $('#warning').text ("Geolocation is not supported by this browser.");
        }
        
    }
});

function stopRKey(evt) {
    var evt = (evt) ? evt : ((event) ? event : null);
    var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
    if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
}

document.onkeypress = stopRKey;