window.addEventListener('scroll', function() {
    var listItems = document.querySelectorAll('#myList li');
    var rect;
    for (var i = 0; i < listItems.length; i++) {
      rect = listItems[i].getBoundingClientRect();
      if (rect.top <= window.innerHeight && rect.bottom >= 0) {
        (function(i) {
          setTimeout(function() {
            listItems[i].style.opacity = '1';
          }, 200 * i);
        })(i);
      }
    }
  });