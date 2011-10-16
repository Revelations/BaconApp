//
//  UpdateController.m
//  BaconApp
//
//  Created by Donovan Hoffman on 1/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "UpdateController.h"
#import "BaconAppDelegate.h"
#import "Update.h"

@implementation UpdateController
@synthesize progBar;

/*-(IBAction)UploadFile:(id)sender{
	NSString * filePath = @"/Users/donovanhoffman/test.txt";
	NSString * urlPath = @"ftp://revelations.webhop.org";
	
	Update * fileRequest = [[Update alloc] init];
	[fileRequest _startSend: filePath : urlPath];
}*/

-(IBAction)Update:(id)sender{
	NSLog(@"Jim is busy looking in a file system.");
	Update *currentFileRequest = [[Update alloc] init];
	[currentFileRequest getDirectory:@"http://revelations.webhop.org:81/"];   
	NSLog(@"Jim has now finished looking for the files");
	[self dismissModalViewControllerAnimated:YES];
	
	
	
	//	TODO: Move to start of program.
	//	Update * updateSession = [[[Update alloc]init]autorelease];
	//	[updateSession GetGameFiles:@"ftp://revelations.webhop.org"];
	//	NSLog(@"Update Session has finished");
	//	
	//	//BaconAppDelegate *appDelegate = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	//	UpdateController *updateController = [[UpdateController alloc] initWithNibName:nil bundle:nil];
	//	[[UIApplication sharedApplication].keyWindow.rootViewController
	//	 presentModalViewController:updateController animated:YES];
	//	[updateController release];
	
}
-(IBAction)CarryOn:(id)sender{
	NSLog(@"Carry on Jim");
	//BaconAppDelegate *appDelegate = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	[self dismissModalViewControllerAnimated:YES];
}



- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
	self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
	if (self) {
		
		//NSString * filePath = @"/Users/donovanhoffman/test.txt";
		//NSString * urlPath = @"ftp://revelations.webhop.org";
		
		//Update * fileRequest = [[Update alloc] init];
		//NSLog(@"hello world I am about to upload with php");
		//[fileRequest uploadPhp: filePath];
		//Custom initialization
	}
	return self;
}

- (void)dealloc
{
	[super dealloc];
}

- (void)didReceiveMemoryWarning
{
	// Releases the view if it doesn't have a superview.
	[super didReceiveMemoryWarning];
	
	// Release any cached data, images, etc that aren't in use.
}

#pragma mark - View lifecycle

- (void)viewDidLoad
{
    self.navigationItem.title = @"Update";
    
	[super viewDidLoad];
	// Do any additional setup after loading the view from its nib.
}

- (void)viewDidUnload
{
	[super viewDidUnload];
	// Release any retained subviews of the main view.
	// e.g. self.myOutlet = nil;
}

- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation
{
	// Return YES for supported orientations
	return (interfaceOrientation == UIInterfaceOrientationPortrait);
}

@end
