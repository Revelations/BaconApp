	//
//  SettingsViewController.m
//  BaconApp
//
//  Created by Donoan Hoffman on 15/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//
#import "Update.h"
#import "UpdateViewController.h"
#import "SettingsViewController.h"


@implementation SettingsViewController

@synthesize txtFontSize, txtIPAddress;


-(IBAction)applyChangesPressed:(id)sender {
	NSLog(@"Applying changes. (Not yet implemented)");
}
-(IBAction)discardChangesPressed:(id)sender {
	NSLog(@"Discarding changes. (Not yet implemented)");
	//Revert font size.
	//Revert server address.
}

-(IBAction)updateButtonPressed:(id)sender {
	//TODO: Move to start of program.
	Update * updateSession = [[[Update alloc]init]autorelease];
	[updateSession GetGameFiles:@"ftp://revelations.webhop.org"];
	NSLog(@"Update Session has finished");
	
	//BaconAppDelegate *appDelegate = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	UpdateViewController *updateController = [[UpdateViewController alloc] initWithNibName:nil bundle:nil];
	[[UIApplication sharedApplication].keyWindow.rootViewController
	 presentModalViewController:updateController animated:YES];
	[updateController release];
	
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
// Implement loadView to create a view hierarchy programmatically, without using a nib.
- (void)loadView {
}
*/


// Implement viewDidLoad to do additional setup after loading the view, typically from a nib.
- (void)viewDidLoad {
    self.navigationItem.title = @"Settings";
    
	[super viewDidLoad];
}


/*
// Override to allow orientations other than the default portrait orientation.
- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation {
	// Return YES for supported orientations.
	return (interfaceOrientation == UIInterfaceOrientationPortrait);
}
*/

- (void) touchesBegan: (NSSet *) touches withEvent: (UIEvent *) event {  
    if (txtFontSize) {  
        if ([txtFontSize canResignFirstResponder]) [txtFontSize resignFirstResponder];  
    }
    if (txtIPAddress) {  
        if ([txtIPAddress canResignFirstResponder]) [txtIPAddress resignFirstResponder];  
    }
    [super touchesBegan: touches withEvent: event];  
}

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


- (void)dealloc {
	[super dealloc];
}


@end
