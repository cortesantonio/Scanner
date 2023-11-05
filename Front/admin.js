function switchView(view){
    views = ['dashboard', 'usuarios','productos','busqueda']
    for(i=0; i<views.length; i++){
        pagina=document.getElementById(views[i]);
        pagina.style.display='none';
        if(view==views[i]){
            pagina.style.display='block';
        }
    }
        
}