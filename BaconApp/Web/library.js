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

// SVG
function SetParams()
{
	var paramArray = [];
	var paramCount = 0;
	if ( document.defaultView.frameElement )
	{
	   var params = document.defaultView.frameElement.getElementsByTagName("param");
		paramCount = params.length;
	   for ( var i = 0, iLen = paramCount; iLen > i; i++ )
	   {
		  var eachParam = params[ i ];
		  var name = eachParam.getAttribute( "name" );
		  var value = eachParam.getAttribute( "value" );
		  
		  paramArray[ name ] = value;
	   }
	}
	
	var light  = document.getElementById("light");
	var label  = document.getElementById("label");
	var marker = document.getElementById("gMarker");
	
	if (paramCount == 0)
	{
		marker.setAttribute( "style", "display:none");
		return;
	}
	
	var color = "red";
	var urhere = "YOU ARE HERE";
	var delta = "translate(0,0)";
	
	//if ( paramArray[ "color" ] != undefined )
		color = paramArray[ "color" ];
	//if ( paramArray[ "label" ] != undefined )
		urhere = paramArray[ "label" ];
	//if ( paramArray[ "x" ] != undefined && paramArray[ "y" ] != undefined ) 
		delta = "translate("+paramArray[ "x" ]+", "+paramArray[ "y" ]+")";

	light.setAttribute( "fill", color );
	label.appendChild( document.createTextNode( urhere ) );
	marker.setAttribute( "transform", delta );
}