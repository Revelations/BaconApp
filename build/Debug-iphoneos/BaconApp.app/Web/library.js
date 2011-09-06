//
//  HistoryItem.m
//  BaconApp
//
//  Created by Donovan Hoffman on 3/09/11.
//  Copyright 2011 Team Bacon. All rights reserved.
//

// Draws a 2d map overlaid by a 'You are here' marker.
function drawMapAndLocation(x,y)
{
    // Create a canvas object suitable for drawing in a WebView.
    var canvas = document.createElement("Canvas");
    canvas.height = "360";
    canvas.width = "300";
    
	// Get the drawing context of the canvas.
    var cxt = canvas.getContext("2d");
    
    // Initialise map and marker images.
    var map=new Image();
    var marker = new Image();
    map.src="./map.png";
    marker.src = "./marker.png";
    
    // Once the map image has finished drawing, overlay it with the marker and a red dot.
    map.onload = function()
    {
        cxt.drawImage(map, 0, 0);
        cxt.drawImage(marker, x + 6, y - 7);
        
        // Why does a Canvas 2d Context contain no method for drawing ellipsis?
        cxt.fillStyle = "rgba(255, 0, 0, 0.8)";  
        cxt.beginPath();
        cxt.arc(x,y,5,0,Math.PI*2,true);
        cxt.closePath();
        cxt.fill();
    }
    
    // Append the new canvas to the body of the html in the WebView.
    document.body.appendChild(canvas);
}

//
function playSound(path)
{
    document.write("<audio id=audioPlayer controls=controls> Your browser does not support the audio element.</audio>");
    document.getElementById('audioPlayer').src = path;
    document.getElementById('audioPlayer').play();
}

// Dynamically applies css to a page.
function addCSS()
{
	var _link = document.createElement('link');
	_link.type = 'text/css';
	_link.rel = 'stylesheet';
	_link.href = 'style.css';
	document.getElementsByTagName("head")[0].appendChild(_link);
}