//
//  UpdateController.m
//  BaconApp
//
//  Created by Donovan Hoffman on 1/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "UpdateViewController.h"
#import "BaconAppDelegate.h"
#import "Update.h"

@implementation UpdateViewController
@synthesize progBar;

/*-(IBAction)UploadFile:(id)sender{
	NSString * filePath = @"/Users/donovanhoffman/test.txt";
	NSString * urlPath = @"ftp://revelations.webhop.org";
	
	Update * fileRequest = [[Update alloc] init];
	[fileRequest _startSend: filePath : urlPath];
}*/

-(IBAction)Update:(id)sender{
	NSString * url = @"http://revelations.webhop.org:81/";
	NSString * gameUrl = @"ftp://revelations.webhop.org/";
	NSLog(@"Jim is busy looking in a file system.");
	Update *currentFileRequest = [[[Update alloc] init] autorelease];
	[currentFileRequest downloadContentFilesFrom:url]; 
	[currentFileRequest GetGameFiles:gameUrl];
	NSLog(@"Jim has now finished looking for the files");
	[self dismissModalViewControllerAnimated:YES];
}

-(IBAction)CarryOn:(id)sender{
	NSLog(@"Carry on Jim");
	//BaconAppDelegate *appDelegate = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	[self dismissModalViewControllerAnimated:YES];
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
