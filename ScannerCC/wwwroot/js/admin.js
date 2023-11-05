function switchView(view) {
  views = ["dashboard", "usuarios", "productos", "busqueda"];
  for (i = 0; i < views.length; i++) {
    pagina = document.getElementById(views[i]);
    pagina.style.display = "none";
    if (view == views[i]) {
      pagina.style.display = "block";
    }
  }
  localStorage.setItem("view", view);
}


if(localStorage.getItem('view') != null){
    switchView(localStorage.getItem('view'))

}
// Para recuperar el estado desde sessionStorage


function showOptionUser(){
    var opciones = document.getElementById('opcionesUser');
    if(opciones.style.display == 'block'){
        opciones.style.display = 'none';
    }else{
        opciones.style.display = 'block';
    }
}
