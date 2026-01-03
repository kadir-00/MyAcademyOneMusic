const audioElements = document.querySelectorAll('.audioPlayer');

audioElements.forEach(audio => {
    audio.addEventListener('play', function () {
        var sonId = $(this).data("id");
        $.ajax({
            url: '/MusicCount/Index/',
            type: 'post',
            data: { id: sonId },
            success: function (data) {
            }
        });
    });
});