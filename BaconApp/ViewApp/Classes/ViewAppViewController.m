//
//  ViewAppViewController.m
//  ViewApp
//
//  Created by Shii Kayano on 1/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "ViewAppViewController.h"
@implementation ViewAppViewController
@synthesize tbc;
-(IBAction) showModalTabBar {
	UIViewController *blueController = [[UIViewController alloc] initWithNibName:nil bundle:nil];
	blueController.view.backgroundColor = [UIColor blueColor];
	blueController.title = @"Blue";
	
	UIViewController *redController = [[UIViewController alloc] initWithNibName:nil bundle:nil];
	redController.view.backgroundColor = [UIColor redColor];

	UIButton *buttonRed = [UIButton buttonWithType:UIButtonTypeRoundedRect];
	UIButton *buttonBlu = [UIButton buttonWithType:UIButtonTypeRoundedRect];
	
	[buttonBlu setFrame:CGRectMake(20.0f, 140.0f, 280.0f, 40.0f)];
	[buttonBlu setTitle:@"Hello" forState:UIControlStateNormal];
	[buttonBlu addTarget:self action:@selector(dismissTabBar) forControlEvents:UIControlEventTouchUpInside];
	
	[buttonRed setFrame:CGRectMake(20.0f, 140.0f, 280.0f, 40.0f)];
	[buttonRed setTitle:@"World" forState:UIControlStateNormal];
	[buttonRed addTarget:self action:@selector(dismissTabBar) forControlEvents:UIControlEventTouchUpInside];
	
	[redController.view addSubview:buttonRed];
	redController.title = @"Red";
	
	[blueController.view addSubview:buttonBlu];
	
	tbc = [[UITabBarController alloc] initWithNibName:nil bundle:nil];
	tbc.viewControllers = [NSArray arrayWithObjects:blueController, redController, nil];
	tbc.selectedViewController = redController;
	NSLog(@"Selected index = %d of %d", tbc.selectedIndex, [tbc.viewControllers count]);
	
	[blueController release];
	[redController release];
	[self presentModalViewController:tbc animated:YES];
}
-(void)dismissTabBar {
	[self dismissModalViewControllerAnimated:YES];
	[tbc release];
}

/*
// The designated initializer. Override to perform setup that is required before the view is loaded.
- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil {
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
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
    // Return YES for supported orientations
    return (interfaceOrientation == UIInterfaceOrientationPortrait);
}
*/

- (void)didReceiveMemoryWarning {
	// Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
	
	// Release any cached data, images, etc that aren't in use.
}

- (void)viewDidUnload {
	// Release any retained subviews of the main view.
	// e.g. self.myOutlet = nil;
}


- (void)dealloc {
    [super dealloc];
}

@end
