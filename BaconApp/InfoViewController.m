//
//  InfoViewController.m
//  BaconApp
//
//  Created by Donoan Hoffman on 6/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "BaconAppDelegate.h"
#import "InfoViewController.h"


@implementation InfoViewController

@synthesize webView;
@synthesize activityIndicator;

// Done button clicked
-(void)dismissView:(id)sender {
	
	// Call the delegate to dismiss the modal view
	[delegate didDismissModalView];
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
}


// Called once the UIWebView is finished loading a page.
//
// Checks if the page loaded is the map page.
// If so, loads the js library into the webview and calls the map drawing function.
-(void) webViewDidFinishLoad:(UIWebView *)webView
{
	NSLog(@"InfoView web did finish load!");
	
	// Make sure the page finished loading is a the map page.
	if(loadingInformation)
	{
//        // Build a string to call js function with a given x, y.
//        NSString * jScriptCall = [NSString stringWithFormat:@"drawMapAndLocation(%d, %d);", interpreter.x, interpreter.y];
//        
//        // Draw the map and marker by evaluating the js function.
//        [self.webView stringByEvaluatingJavaScriptFromString:jScriptCall];
//        
		// Finished load of map screen.
		loadingInformation = false;
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


// Implement viewDidLoad to do additional setup after loading the view, typically from a nib.
- (void)viewDidLoad {
    self.navigationItem.title = @"Information";
    
	[super viewDidLoad];
}

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

- (void)viewWillAppear:(BOOL)animated {
	[super viewWillAppear:(BOOL)animated];
	
	
	//BaconAppDelegate *appDelegate = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	
	//[self webViewLoadPage: appDelegate.model.current.htmlFile];
	BaconAppDelegate *appDelegate = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];

	NSLog(@"Page to load is: %@", appDelegate.html);
	
	[self webViewLoadPage:appDelegate.html];
}
	 

- (void)dealloc {
	[super dealloc];
}


@end
