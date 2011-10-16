//
//  NavigationView.m
//  BaconApp
//
//  Created by Donovan Hoffman on 14/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "NavigationView.h"

@implementation NavigationView

#pragma mark - Properties
@synthesize aboutView;
@synthesize scannerView;
@synthesize mapView;
@synthesize feedbackView;
@synthesize gameView;
@synthesize settingsView;
@synthesize helpView;
@synthesize infoView;
@synthesize titleView;

@synthesize currentViewController;

- (void)didDismissModalView {
    // Dismiss the modal view controller
    [self dismissModalViewControllerAnimated:YES];
}


#pragma mark  - App Admin
- (id)initWithStyle:(UITableViewStyle)style
{
	self = [super initWithStyle:style];
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
- (void)viewDidLoad
{
	cellContent = [[NSArray arrayWithObjects:
					@"Title",
					@"Scan",
					@"Information",
					@"Map",
					@"Feedback",
					@"Game",
					@"Settings",
                    @"About",
					@"Help",
					nil] retain];
	self.navigationItem.title = @"BACON!";
	// Uncomment the following line to preserve selection between presentations.
	// self.clearsSelectionOnViewWillAppear = NO;
	// Uncomment the following line to display an Edit button in the navigation bar for this view controller.
	// self.navigationItem.rightBarButtonItem = self.editButtonItem;

	[super viewDidLoad];
}

- (void)viewDidUnload
{
	[super viewDidUnload];
	// Release any retained subviews of the main view.
	// e.g. self.myOutlet = nil;
}

- (void)viewWillAppear:(BOOL)animated
{
	[super viewWillAppear:animated];;
}

- (void)viewDidAppear:(BOOL)animated
{
	[super viewDidAppear:animated];
}

- (void)viewWillDisappear:(BOOL)animated
{
	[super viewWillDisappear:animated];
}

- (void)viewDidDisappear:(BOOL)animated
{
	[super viewDidDisappear:animated];
}

- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation
{
	// Return YES for supported orientations
	return (interfaceOrientation == UIInterfaceOrientationPortrait);
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section
{
		return [cellContent count];
}


//configure the appearance of the cells
- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath
{
	static NSString *CellIdentifier = @"Cell";
	
	UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:CellIdentifier];
	if (cell == nil) {
		cell = [[[UITableViewCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:CellIdentifier] autorelease];
	}
	
	// Configure the cell...
	NSUInteger row = [indexPath row];
	cell.textLabel.text =[cellContent objectAtIndex:row];
	
	return cell;
}


-(void)showSomeView:(ModalViewController *)viewController {
	
	// We are the delegate responsible for dismissing the modal view 
	viewController.delegate = self;

	// Create a Navigation controller
	UINavigationController * navController = [[UINavigationController alloc] initWithRootViewController:viewController];
    navController.navigationBar.barStyle = UIBarStyleBlackOpaque;
	
	// show the navigation controller modally
	[self presentModalViewController:navController animated:YES];
	
	// Clean up resources
	[navController release];
}

-(void)showCurrentView {
	if (currentViewController) {
		[self showSomeView:self.currentViewController];
	}
}

-(void)showAboutView {
	// Create the modal view controller
	if (!self.aboutView) {
		AboutViewController *viewController = [[AboutViewController alloc] initWithNibName:@"AboutView" bundle:nil];
		self.aboutView = viewController;
		currentViewController = viewController;
		// Cleanup resources
		[viewController release];
	}
    else currentViewController = aboutView;
    
	[self showCurrentView];
}
-(void)showScannerView {
	// Create the modal view controller
	if (!self.scannerView) {
		ScannerViewController *viewController = [[ScannerViewController alloc] initWithNibName:@"ScannerView" bundle:nil];
		self.scannerView = viewController;
		currentViewController = viewController;
		// Cleanup resources
		[viewController release];
	}
    else currentViewController = scannerView;
    
	[self showCurrentView];
}
-(void)showInfoView {
	// Create the modal view controller
	if (!self.infoView) {
		InfoViewController *viewController = [[InfoViewController alloc] initWithNibName:@"InfoView" bundle:nil];
		self.infoView = viewController;
		currentViewController = viewController;
		// Cleanup resources
		[viewController release];
	}
    else currentViewController = infoView;
    
	[self showCurrentView];
}

-(void)showMapView {
	// Create the modal view controller
	if (!self.mapView) {
		MapViewController *viewController = [[MapViewController alloc] initWithNibName:@"MapView" bundle:nil];
		self.mapView = viewController;
		currentViewController = viewController;
		// Cleanup resources
		[viewController release];
	}
    else currentViewController = mapView;
    
	[self showCurrentView];
}
-(void)showFeedbackView {
	// Create the modal view controller
	if (!self.feedbackView) {
		FeedbackController *viewController = [[FeedbackController alloc] initWithNibName:@"FeedbackView" bundle:nil];
		self.feedbackView = viewController;
		currentViewController = viewController;
		// Cleanup resources
		[viewController release];
	}
    else currentViewController = feedbackView;
    
	[self showCurrentView];
}
-(void)showGameView {
	// Create the modal view controller
	if (!self.gameView) {
		GameViewController *viewController = [[GameViewController alloc] initWithNibName:@"GameView" bundle:nil];
		self.gameView = viewController;
		currentViewController = viewController;
		// Cleanup resources
		[viewController release];
	}
    else currentViewController = gameView;
    
	[self showCurrentView];
}
-(void)showSettingsView {
	// Create the modal view controller
	if (!self.settingsView) {
		SettingsViewController *viewController = [[SettingsViewController alloc] initWithNibName:@"SettingsView" bundle:nil];
		self.settingsView = viewController;
		currentViewController = viewController;
		// Cleanup resources
		[viewController release];
	}
    else currentViewController = settingsView;
    
	[self showCurrentView];
}
-(void)showHelpView {
	if (!helpView) {
		// Create the modal view controller
		HelpViewController *viewController = [[HelpViewController alloc] initWithNibName:@"HelpView" bundle:nil];
		self.helpView = viewController;
		currentViewController = viewController;
		// Cleanup resources
		[viewController release];
	}
    else currentViewController = helpView;
    
	[self showCurrentView];
}

-(void)showTitleView {
	if (!titleView) {
		// Create the modal view controller
		TitleViewController *viewController = [[TitleViewController alloc] initWithNibName:@"TitleView" bundle:nil];
		self.titleView = viewController;
		currentViewController = viewController;
		// Cleanup resources
		[viewController release];
	}
    else currentViewController = titleView;
                     
    [self showCurrentView];
}

//navigation and event handling
- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
	switch (indexPath.row) {
        case 0:
            NSLog(@"You have selected TitleView");
            [self showTitleView];
            break;
		case 1:
			NSLog(@"You have selected ScanView");
			[self showScannerView];
			break;
		case 2:
			NSLog(@"You have selected InfoView");
			[self showInfoView];
			break;
		case 3:
			NSLog(@"You have selected MapView");
			[self showMapView];
			break;
		case 4:
			NSLog(@"You have selected FeedbackView");
			[self showFeedbackView];
			break;
		case 5:
			NSLog(@"You have selected GameView");
			[self showGameView];
			break;
		case 6:
			NSLog(@"You have selected SettingsView");
			[self showSettingsView];
			break;
		case 7:
			NSLog(@"You have selected AboutView");
			[self showAboutView];
			break;
		case 8:
			NSLog(@"You have Selected HelpView");
			[self showHelpView];
			break;
		default:
			NSLog(@"You have selected something else");
			exit(-1);
			break;
	}
}

@end
