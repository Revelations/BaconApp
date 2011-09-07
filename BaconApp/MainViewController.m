//
//  BaconAppAppDelegate.m
//  BaconApp
//
//  Created by Jordan on 31/08/11.
//  Copyright 2011 Team Bacon. All rights reserved.
//

#import "MainViewController.h"
#import "BaconAppDelegate.h"

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
    
    self.webView.delegate = self;

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
    interpreter.storedInputString = scanner.ouputString;    
    
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


// Changes the page of the UIWebview to a given string.
//
// Builds a string with the given input, checks if that file exists, and loads it
// into the WebView if so.
-(void) webViewLoadPage:(NSString *) inputString
{
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
	// addCSS() becomes obsolete IF webpage authors link CSS to each page.
	[self.webView stringByEvaluatingJavaScriptFromString:@"addCSS();"]; 
	
}


// Called once the UIWebView is finished loading a page.
//
// Checks if the page loaded is the map page.
// If so, loads the js library into the webview and calls the map drawing function.
-(void) webViewDidFinishLoad:(UIWebView *)webView
{
    // Make sure the page finished loading is a the map page.
    if(loadingMapScreen)
    {
        // Build a string to call js function with a given x, y.
        NSString * jScriptCall = [NSString stringWithFormat:@"drawMapAndLocation(%d, %d);", interpreter.x, interpreter.y];
        
        // Draw the map and marker by evaluating the js function.
        [self.webView stringByEvaluatingJavaScriptFromString:jScriptCall];
        
        // Finished load of map screen.
        loadingMapScreen = false;
    }
    
    // Stop animating activity indicator.
    [activityIndicator stopAnimating];
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
