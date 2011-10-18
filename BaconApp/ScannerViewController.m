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
#import "Update.h"
#import "SettingsViewController.h"
#import "InfoViewController.h"

@implementation ScannerViewController

@synthesize resultText;
@synthesize resultImage; // TODO: debate whther we should keep.
@synthesize interpreter, current;

-(void)addToViewsSeen:(NSString *) file {
	BaconAppDelegate * appDelegate = (BaconAppDelegate *) [[UIApplication sharedApplication] delegate];
	
	NSArray * contents = [file componentsSeparatedByString:@"."];
	NSString * currentFile = [contents objectAtIndex:0];
	
	NSMutableArray * currentP = [appDelegate scannedItems];
	BOOL found = NO;
	for (NSString * s in currentP) {
		if ([s isEqualToString: currentFile]) {
			found = YES;
			break;
		}
	}
	
	if(!found) {
		[appDelegate addToScannedCodes:interpreter.htmlPath];
	}
	[appDelegate setPage_title:interpreter.htmlPath];
	NSLog(@"scanned count: %i", [appDelegate.scannedItems count]);
	NSLog(@"interpreter.htmPath = %@", interpreter.htmlPath);
	
	NSLog(@"scanned count: %i", [appDelegate.scannedItems count]);
}

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

// TODO: See if we can refactor the output from scanner to DataModel.
- (void) imagePickerController: (UIImagePickerController*) reader
 didFinishPickingMediaWithInfo: (NSDictionary*) info
{

	NSLog(@"Scan did finish picking");
	// ADD: get the decode results
	id<NSFastEnumeration> results =
	[info objectForKey: ZBarReaderControllerResults];
	ZBarSymbol *symbol = nil;
	for(symbol in results)
		// EXAMPLE: just grab the first barcode
		break;
	
	// EXAMPLE: do something useful with the barcode data

	resultText.text = @"The scan was successful!";

	BaconAppDelegate *appDelegate = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	

	// EXAMPLE: do something useful with the barcode image
	resultImage.image =
		[info objectForKey: UIImagePickerControllerOriginalImage]; // Should we keep this? Memory/GC may be issue here.
	
	// ADD: dismiss the controller (NB dismiss from the *reader*!)
	[reader dismissModalViewControllerAnimated: YES];
	
	
	// Retrieve scanner results.
	interpreter = [Interpreter new];
	NSLog(@"%@", symbol.data);

	[interpreter setVals: symbol.data];
	//interpreter.storedInputString = symbol.data;
	NSLog(@"%@", symbol.data);

	current = [[HistoryItem alloc] initWithHtmlFile:[interpreter htmlPath] x:interpreter.x y:interpreter.y];

	// TODO: We could try use history here, that is, go to last page.
	// OR, we could let the user choose.

	//NSLog(@"");
	appDelegate.x	= interpreter.x;
	appDelegate.y	= interpreter.y;
	appDelegate.html = interpreter.htmlPath;
	[self addToViewsSeen:interpreter.htmlPath];
	NSLog(@"view count:%i", [appDelegate.scannedItems count]);	
	
	//self.tabBarController.selectedIndex = 1;
	
	InfoViewController *vc = [[[InfoViewController alloc] initWithNibName:@"InfoView" bundle:[NSBundle mainBundle]] autorelease];
	[self.navigationController pushViewController:vc animated:YES];	
	
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
    self.navigationItem.title = @"Scanner";
    
	[super viewDidLoad];
	NSLog(@"Scanner view did load");
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

// Called when the view disappears.
-(void)viewWillDisappear:(BOOL)animated {
	[super viewWillDisappear:animated];
	// Set the current history item to interpreted scanner data.
	
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

@end
