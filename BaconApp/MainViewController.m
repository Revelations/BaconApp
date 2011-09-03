//
//  BaconAppAppDelegate.m
//  BaconApp
//
//  Created by Jordan on 31/08/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "MainViewController.h"

@implementation MainViewController

NSString * const MENU_HTML_FILE = @"Menu";

@synthesize scanButton, mapButton, resultText, scanner, interpreter, history, current;
@synthesize window=_window;
@synthesize webView=_webView;


- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions
{
    // Override point for customization after application launch.
    scanner = [Scanner new];
    interpreter = [Interpreter new];
    
    current = [HistoryItem new];
    current.htmlFile = MENU_HTML_FILE;
    current.x = 0;
    current.y = 0;
    
    [history addObject:current];
    
    [self webViewLoadPage:current.htmlFile];
    
    [self.window makeKeyAndVisible];
    return YES;
}


// Called when a user presses the scan button.
//
//
//
-(IBAction) scanButtonPressed 
{
    // Initiate a scan.
    [scanner scan:self];
    
    // Retrieve scanner results.
    interpreter.storedInputString = scanner.ouputString;
    
    // Set the current history item to interpreted scanner data.
    current = [[HistoryItem alloc] initHtmlFile:[interpreter htmlPath] x:[interpreter xCoord] y:[interpreter yCoord]];
    
    // Add the current history item to the list.
    [history addObject:current];
    
    [self webViewLoadPage:current.htmlFile];
}

// Called when a user presses the map button.
//
// Loads a local html map file containing a map image. Superimposes a
// smaller 'you are here' image over the main image using a JS script.
-(IBAction) mapButtonPressed
{
    
}

// Changes the page of the UIWebview to a given string.
//
//
//
-(void) webViewLoadPage:(NSString *) inputString
{
    NSURLRequest * request = [NSURLRequest requestWithURL:[NSURL fileURLWithPath:[[NSBundle mainBundle] pathForResource:inputString ofType:@"html"]]];
    
    [self.webView loadRequest:request];
    
    
    /*
     *  TODO: Implement JS handler here.
     *
     *  Also check validity of a file path before displaying it.
     */
}

- (void)applicationWillResignActive:(UIApplication *)application
{

}

- (void)applicationDidEnterBackground:(UIApplication *)application
{
   
}

- (void)applicationWillEnterForeground:(UIApplication *)application
{
   
}

- (void)applicationDidBecomeActive:(UIApplication *)application
{
    
}

- (void)applicationWillTerminate:(UIApplication *)application
{

}

- (void)dealloc
{
    [scanner release];
    [interpreter release];
    
    [_window release];
    [super dealloc];
}

@end
