/**
 * Used for drawing map marker.
 *
 * Reference: http://homepage.ntlworld.com/leon.stringer/cs/ACIT/Using_SVG_for_Interactive_Maps.pdf
 */

// Returns the map svg object.
function getMapObj() {
	var svgObject = document.getElementById('mapobj');
	if (svgObject && svgObject.contentDocument)
		// All other browsers
		svgObject = svgObject.contentDocument;
	else
		// Internet Explorer + Adobe SVG Viewer
		svgObject = svgObject.getSVGDocument();
	return svgObject;
}

function initMarker()
{
	var svgObject = getMapObj();
		
	var marker = svgObject.getElementById( 'gMarker' );
	
	//if (document.getElementById('checkbox').checked) // Here, an example to use a form control to toggle visibility.
	//if ( true )
	//marker.setAttribute( 'visibility', 'inherit' );
	//else
	  marker.setAttribute( 'visibility', 'hidden' );

	var pinhead = svgObject.getElementById( 'pinhead' );
	var pintext = svgObject.getElementById( 'pintext' );
	pinhead.setAttribute( 'fill', 'url(#Shiny)' );
	pintext.appendChild( document.createTextNode( 'You are here' ) );
}

//Moves the map marker to the x,y coordinate, relative to the image.
function moveMarkerTo(x,y)
{
	var marker = getMapObj().getElementById( 'gMarker' );
	if ( marker.getAttribute( 'visibility' ) == 'hidden' )
		marker.setAttribute( 'visibility', 'inherit' );
	marker.setAttribute( 'transform', 'translate('+ x +','+ y +')' );
}