    //
//  TriButtonContoller.m
//  ViewApp
//
//  Created by Russell Fredericks on 3/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "TriButtonContoller.h"

@implementation TriButtonContoller
@synthesize mapview;

-(IBAction) scanbuttonpressed:(id) sender{
	//this calls jordans stuff?
	NSLog(@"This Scans stuff");
}

-(IBAction) infobuttonpressed:(id) sender{
	//gets url and uses it to find the html file it should load??
	NSLog(@"This is INFO");
}

-(IBAction) mapbuttonpressed:(id) sender{
	//does some jscrit stuff in the webview that  made???
	NSLog(@"This makes a MAP");
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


- (void)dealloc {
	[mapview release];
    [super dealloc];
}


@end
