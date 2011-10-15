//
//  AboutViewController.m
//  BaconApp
//
//  Created by Donovan Hoffman on 15/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "AboutViewController.h"

NSString * const facebook = @"http://www.facebook.com/SavingMaungatautari";
NSString * const website = @"http://www.maungatrust.org/";

@implementation AboutViewController

-(IBAction)btnFacebookPressed:(id)sender {
	NSLog(@"Navigating to \"%@\"", facebook);
	
	NSString *urlString = facebook;
	[[UIApplication sharedApplication] openURL:[NSURL URLWithString:urlString]];
}

-(IBAction)btnWebsitePressed:(id)sender {
	NSLog(@"Navigating to \"%@\" (Function not yet implemented)", website);
	
	NSString *urlString = website;
	[[UIApplication sharedApplication] openURL:[NSURL URLWithString:urlString]];
}

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
	self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
	if (self) {
		// Custom initialization
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

/*
// Implement loadView to create a view hierarchy programmatically, without using a nib.
- (void)loadView
{
}
*/

// Implement viewDidLoad to do additional setup after loading the view, typically from a nib.
- (void)viewDidLoad
{
	[super viewDidLoad];
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
