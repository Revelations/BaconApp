//
//  BaconAppAppDelegate.m
//  BaconApp
//
//  Created by Jordan on 31/08/11.
//  Copyright 2011 Team Bacon. All rights reserved.
//

#import "MainViewController.h"
#import "BaconAppDelegate.h"
#import "UpdateController.h"

@implementation MainViewController

@synthesize scanButton, mapButton, resultText, scanner, interpreter, history, current;
@synthesize window=_window;
@synthesize webView=_webView;

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions
{
	// Override point for customization after application launch.
	
	scanner = [Scanner new];
	interpreter = [Interpreter new];
	
	
	current = [[[HistoryItem alloc] initWithHtmlFile:MENU_HTML_FILE x:0 y:0] autorelease];
	
	loadingMapScreen = false;
	
	[history addObject:current];
	
	[self webViewLoadPage:current.htmlFile];
	
	//Does nothing?
	//self.webView.delegate = self;

	[self.window makeKeyAndVisible];
	
	return YES;
}


// Called when a user presses the scan button.
//
// Calls the scanner to initiate a scan, the interpreter to break up it's results,
// creates a history node to store results, and finally brings up the associated page.
-(IBAction) scanButtonPressed
{
	// Animate activity indicator.
	[activityIndicator startAnimating];
	
	// Initiate a scan.
	[scanner scan:self];
	
	// Retrieve scanner results.
	[interpreter setVals: scanner.ouputString];
	//interpreter.storedInputString = scanner.ouputString;    
	
	// Set the current history item to interpreted scanner data.
	current = [[HistoryItem alloc] initWithHtmlFile:[interpreter htmlPath] x:interpreter.x y:interpreter.y];

	// Add the current history item to the list.
	[history addObject:current];
	
	// Load the html file associated with the scanned item.
	[self webViewLoadPage:current.htmlFile];
}


// Called when a user presses the map button.
//
// Loads a local html map file containing a map image. Superimposes a smaller
// 'you are here' image over the main image using a JS script.
-(IBAction) mapButtonPressed
{
	// Load the (mostly blank) map page.
	[self webViewLoadPage: MAP_HTML_FILE];
	
	// Flag to run js once page has loaded.
	loadingMapScreen = true;
}


// Loads a given html file into the UIWebView.
-(void) webViewLoadPage:(NSString *) inputString
{
}

//
- (void)dealloc
{
	[scanner release];
	[interpreter release];
	
	[_window release];
	[super dealloc];
}


@end
