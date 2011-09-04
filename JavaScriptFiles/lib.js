function playSound(path){
  document.write("<audio id=audioPlayer controls=controls> Your browser does not support the audio element.</audio>");
  document.getElementById('audioPlayer').src = path;
  document.getElementById('audioPlayer').play();
}

function drawImageAndLocation(x,y){
	var c=document.getElementById("mapCanvas");
	var cxt=c.getContext("2d");
	var map=new Image();
	
	map.onload = function(){
  		cxt.drawImage(map, 0, 0);
		cxt.fillStyle = "rgba(0, 0, 200, 0.3)";  
		cxt.beginPath();
		cxt.arc(x,y,15,0,Math.PI*2,true);//x,y,radius,startingAngle,EndingAngle,anti-clockwise
		cxt.closePath();
		cxt.fill();
	}
	map.src="./small.png";
}

function buildMap(x, y){

	var canvas = document.createElement("canvas");
	canvas.setAttribute("id","mapCanvas");
	canvas.setAttribute("width","800");
	canvas.setAttribute("height", "800");
	canvas.setAttribute("style", "border:1px solid #c3c3c3;");
	var canvasText = document.createTextNode("Your browser does not support the canvas element");
	canvas.appendChild(canvasText);
	
	document.body.appendChild(canvas);
	
	drawImageAndLocation(x,y)
}
