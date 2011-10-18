//
//  AnswerSelectionController.m
//  BaconApp
//
//  Created by Donoan Hoffman on 16/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "AnswerSelectionController.h"
#import "BaconAppDelegate.h"

@implementation AnswerSelectionController
@synthesize question;
@synthesize my_parent;

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



-(IBAction)btnOptionAPressed:(id)sender{
	NSLog(@"A has been pushed");
	if (my_parent == nil)
		NSLog(@"parent is nil");
	else {
		NSLog(@"parent is not nil");
	}

	int index = [question intValue];
	NSLog(@"index is %i", index);
	BaconAppDelegate * delgato = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	[delgato updateAnswersWith:[@"0" retain] At: question];
	//[my_parent updateAnswersWith:@"0a" At: index];
	[self.navigationController popViewControllerAnimated:YES];
}

-(IBAction)btnOptionBPressed:(id)sender{
	NSLog(@"B has been pushed");
	if (my_parent == nil)
		NSLog(@"parent is nil");
	else {
		NSLog(@"parent is not nil");
	}
	
	int index = [question intValue];
	NSLog(@"index is %i", index);
	BaconAppDelegate * delgato = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	[delgato updateAnswersWith:[@"1" retain] At: question];
	//[my_parent updateAnswersWith:@"0a" At: index];
	[self.navigationController popViewControllerAnimated:YES];
}


-(IBAction)btnOptionCPressed:(id)sender{
	NSLog(@"C has been pushed");
	if (my_parent == nil)
		NSLog(@"parent is nil");
	else {
		NSLog(@"parent is not nil");
	}
	
	int index = [question intValue];
	NSLog(@"index is %i", index);
	BaconAppDelegate * delgato = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	[delgato updateAnswersWith:[@"2"retain] At: question];
	//[my_parent updateAnswersWith:@"0a" At: index];
	[self.navigationController popViewControllerAnimated:YES];
}


-(IBAction)btnOptionDPressed:(id)sender{
	NSLog(@"D has been pushed");
	if (my_parent == nil)
		NSLog(@"parent is nil");
	else {
		NSLog(@"parent is not nil");
	}
	
	int index = [question intValue];
	NSLog(@"index is %i", index);
	BaconAppDelegate * delgato = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	[delgato updateAnswersWith:[@"3" retain] At: question];
	//[my_parent updateAnswersWith:@"0a" At: index];
	[self.navigationController popViewControllerAnimated:YES];
}


//-(void)

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
