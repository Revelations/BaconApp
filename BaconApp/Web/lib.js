function playSound(path){
  document.write("<audio id=audioPlayer controls=controls> Your browser does not support the audio element.</audio>");
  document.getElementById('audioPlayer').src = path;
  document.getElementById('audioPlayer').play();
}

function drawMapAndLocation(x,y){
	var c=document.getElementById("mapCanvas");
	var cxt=c.getContext("2d");
	var map=new Image();
    var marker = new Image();
	
	map.onload = function(){
  		cxt.drawImage(map, 0, 0);
        cxt.drawImage(marker, x + 6, y - 7);
		cxt.fillStyle = "rgba(255, 0, 0, 0.8)";  
		cxt.beginPath();
		cxt.arc(x,y,5,0,Math.PI*2,true);//x,y,radius,startingAngle,EndingAngle,anti-clockwise
		cxt.closePath();
		cxt.fill();
	}
	map.src="./map.png";
    marker.src = "./marker.png";
}
