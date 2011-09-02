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
