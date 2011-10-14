//
//  MapViewController.m
//  BaconApp
//
//  Created by Donoan Hoffman on 6/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "BaconAppDelegate.h"
#import "MapViewController.h"


@implementation MapViewController

@synthesize webView;

-(void) webViewLoadPage:(NSString *) inputString
{
	NSLog(@"webViewLoadPage");
	
    // Animate activity indicator.
    [activityIndicator startAnimating];
    
    // Get the file path of the requested html file.
    NSString * filePath = [[NSBundle mainBundle] pathForResource:inputString ofType:@"html" inDirectory:WEB_DIRECTORY];
	
    // If that file doesn't exist then break prematurely.
    if(![[NSFileManager defaultManager] fileExistsAtPath:filePath])
        return;
    
    // Create and load the request.
    NSURLRequest * request = [NSURLRequest requestWithURL:[NSURL fileURLWithPath:filePath]];
	
    [self.webView loadRequest:request];
	
	// Draw the map and marker by evaluating the js function.
	//[self.webView stringByEvaluatingJavaScriptFromString:@"addCSS();"];
	
}


// Called once the UIWebView is finished loading a page.
//
// Checks if the page loaded is the map page.
// If so, loads the js library into the webview and calls the map drawing function.
-(void) webViewDidFinishLoad:(UIWebView *)webView
{
	NSLog(@"MapView web did finish load! loadingMapScreen: %d", loadingMapScreen);

    // Make sure the page finished loading is a the map page.
    if(loadingMapScreen)
    {
		//NSLog(@"Before JS is called, interpreter is %@ and has coordinates: %d, %d", interpreter, interpreter.x, interpreter.y);

		BaconAppDelegate *appDelegate = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
		NSLog(@"Before JScript is called, appDelegate is %@ and has coordinates: %d, %d", appDelegate, appDelegate.x, appDelegate.y);
				
        // Build a string to call js function with a given x, y.
        NSString * jScriptCall = [NSString stringWithFormat:@"drawMapAndLocation(%d, %d);", appDelegate.x, appDelegate.y];
        
        // Draw the map and marker by evaluating the js function.
        [self.webView stringByEvaluatingJavaScriptFromString:jScriptCall];
        
        // Finished load of map screen.
        loadingMapScreen = false;
    }
    
    // Stop animating activity indicator.
    [activityIndicator stopAnimating];
}



// The designated initializer.  Override if you create the controller programmatically and want to perform customization that is not appropriate for viewDidLoad.
/*
- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil {
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization.
    }
    return self;
}
*/

/*
// Implement viewDidLoad to do additional setup after loading the view, typically from a nib.
- (void)viewDidLoad {
    [super viewDidLoad];
}
*/

/*
// Override to allow orientations other than the default portrait orientation.
- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation {
    // Return YES for supported orientations.
    return (interfaceOrientation == UIInterfaceOrientationPortrait);
}
*/

- (void)didReceiveMemoryWarning {
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
    
    // Release any cached data, images, etc. that aren't in use.
}

- (void)viewDidUnload {
    [super viewDidUnload];
    // Release any retained subviews of the main view.
    // e.g. self.myOutlet = nil;
}

-(void)viewWillAppear:(BOOL)animated {
	NSLog(@"mapViewWillAppear");
	
	// Load the (mostly blank) map page.
    [self webViewLoadPage:MAP_HTML_FILE];
    
	NSLog(@"Page to load is: %@", MAP_HTML_FILE);
	
    // Flag to run js once page has loaded.
    loadingMapScreen = true;
}

- (void)dealloc {
    [super dealloc];
}


@end
