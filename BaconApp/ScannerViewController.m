//
//  ScannerViewController.m
//  BaconApp
//
//  Created by Donoan Hoffman on 6/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "ScannerViewController.h"
#import "Interpreter.h"
#import "BaconAppDelegate.h"

@implementation ScannerViewController

@synthesize resultImage, resultText;

-(IBAction) scanButtonPressed
{
	NSLog(@"Scan button pressed");
	ZBarReaderViewController *reader = [ZBarReaderViewController new];
	reader.readerDelegate = self;
	reader.supportedOrientationsMask = ZBarOrientationMaskAll;
	
	ZBarImageScanner *scanner = reader.scanner;
	//TODO: (optional) additional reader configuration here
	
	// Example: disable rarely used I2/5 to improve performance
	[scanner setSymbology: ZBAR_I25
			 config: ZBAR_CFG_ENABLE
			 to: 0];
	
	//present and release the controller
	[self presentModalViewController: reader
		  animated:YES];
	[reader release];
	
}

- (void) imagePickerController: (UIImagePickerController*) reader
 didFinishPickingMediaWithInfo: (NSDictionary*) info
{
    // ADD: get the decode results
    id<NSFastEnumeration> results =
	[info objectForKey: ZBarReaderControllerResults];
    ZBarSymbol *symbol = nil;
    for(symbol in results)
        // EXAMPLE: just grab the first barcode
        break;
	
    // EXAMPLE: do something useful with the barcode data
    resultText.text = @"Scan was successful!";
	
    // EXAMPLE: do something useful with the barcode image
    resultImage.image =
	[info objectForKey: UIImagePickerControllerOriginalImage];
 	
    // ADD: dismiss the controller (NB dismiss from the *reader*!)
    [reader dismissModalViewControllerAnimated: YES];
	
	
	
	//////////
	
    // Retrieve scanner results.
	Interpreter *interpreter = [Interpreter new];
    interpreter.storedInputString = symbol.data;
    
	// To BaconTeam: Currently, it says that page string is null. 
	// So, somehow, it's not passing in the string to the datamodel
	// TODO: Get passing data to and getting data from data model working
	// ~~~~ Shii
	
	
    // Set the current history item to interpreted scanner data.
	BaconAppDelegate *appDelegate = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	appDelegate.model.current = [[HistoryItem alloc] initWithHtmlFile:[interpreter htmlPath] x:interpreter.x y:interpreter.y];
	
    // Add the current history item to the list.
    [appDelegate.model.history addObject:appDelegate.model.current];
    
    // Load the html file associated with the scanned item.
    [self webViewLoadPage:@"Hello"];
	
	NSLog(@"ScannerView says, Model page is: %@", appDelegate.model.current.htmlFile);
	
	//model.current = current;
	
}

- (void) webViewLoadPage:(NSString *)path {
	NSLog(@"%d", [self.tabBarController selectedIndex]);
	self.tabBarController.selectedIndex = 1;
//	// Animate activity indicator.
//    [activityIndicator startAnimating];
//    
//    // Get the file path of the requested html file.
//    NSString * filePath = [[NSBundle mainBundle] pathForResource:inputString ofType:@"html" inDirectory:@"Web"];
//	
//    // If that file doesn't exist then break prematurely.
//    if(![[NSFileManager defaultManager] fileExistsAtPath:filePath])
//        return;
//    
//    // Create and load the request.
//    NSURLRequest * request = [NSURLRequest requestWithURL:[NSURL fileURLWithPath:filePath]];
//	
//    [self.webView loadRequest:request];
//	
//	// Draw the map and marker by evaluating the js function.
//	[self.webView stringByEvaluatingJavaScriptFromString:@"addCSS();"];	
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

- (void) dealloc
{
    self.resultImage = nil;
    self.resultText = nil;
    [super dealloc];
}

- (BOOL) shouldAutorotateToInterfaceOrientation: (UIInterfaceOrientation) interfaceOrientation
{
    return(YES);
}

@end
