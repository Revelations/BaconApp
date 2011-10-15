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
#import "UpdateController.h"
#import "Update.h"
#import "SettingsViewController.h"

@implementation ScannerViewController

@synthesize resultImage, resultText, interpreter, current;

-(IBAction) scanButtonPressed:(id)sender
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
-(IBAction) settingsButtonPressed:(id)sender
{
	//	SettingsController *setting = [[SettingsController alloc]init];
	/*NSLog(@"HI? u want settings?");
	BaconAppDelegate *appDelegate = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	
	SettingsController *settingsController = [[SettingsController alloc] initWithNibName:nil bundle:nil];
	[[UIApplication sharedApplication].keyWindow.rootViewController
	 presentModalViewController:settingsController animated:YES];
	[settingsController release];*/
	
	
	//	[self presentModalViewController:setting animated:YES];
	//	[setting release];
}

// TODO: See if we can refactor the output from scanner to DataModel.

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
	
	
	// Retrieve scanner results.
	interpreter = [Interpreter new];
	[interpreter setVals: symbol.data];
	//interpreter.storedInputString = symbol.data;
	
	current = [[HistoryItem alloc] initWithHtmlFile:[interpreter htmlPath] x:interpreter.x y:interpreter.y];

	// TODO: We could try use history here, that is, go to last page.
	// OR, we could let the user choose.
	self.tabBarController.selectedIndex = 1;
	
	//model.current = current;
	
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
	NSLog(@"Scanner view did load");
	Update * updateSession = [[[Update alloc]init]autorelease];
	[updateSession GetGameFiles:@"ftp://revelations.webhop.org/"];
	NSLog(@"Update Session has finished");
	[super viewDidLoad];
	
	//BaconAppDelegate *appDelegate = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	UpdateController *updateController = [[UpdateController alloc] initWithNibName:nil bundle:nil];
	[[UIApplication sharedApplication].keyWindow.rootViewController
	 presentModalViewController:updateController animated:YES];
	[updateController release];
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


-(void)addToViewsSeen:(NSString *) file{
	
	NSArray * contents = [[file componentsSeparatedByString:@"."] autorelease];
	NSString * currentFile = [[contents objectAtIndex:0] autorelease];
	
	NSMutableArray * currentP = [(BaconAppDelegate *) [[UIApplication sharedApplication] delegate]scannedItems];
	
	BOOL found = NO;
	for (NSString * s in currentP) {
		if (s == currentFile) {
			found = YES;
			break;
		}
	}
	
	if(!found){
		[currentP addObject : currentFile];
	}
								
	
	
	[[(BaconAppDelegate *)[[UIApplication sharedApplication] delegate] scannedItems] addObject:interpreter.htmlPath];
}

// Called when the view disappears.

-(void)viewWillDisappear:(BOOL)animated {
	[super viewWillDisappear:animated];
	// Set the current history item to interpreted scanner data.
	BaconAppDelegate *appDelegate = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	
	appDelegate.x = interpreter.x;
	appDelegate.y = interpreter.y;
	appDelegate.html = interpreter.htmlPath;
	[self addToViewsSeen:interpreter.htmlPath];
	
	
	//appDelegate.model.current = [[HistoryItem alloc] initWithHtmlFile:[interpreter htmlPath] x:interpreter.x y:interpreter.y];
	// Add the current history item to the list.
	//[appDelegate.model.history addObject:appDelegate.model.current];
	
	
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
