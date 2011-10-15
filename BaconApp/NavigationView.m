//
//  NavigationView.m
//  BaconApp
//
//  Created by Donovan Hoffman on 14/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "NavigationView.h"
#import "AboutViewController.h"
#import "ScannerViewController.h"
#import "MapViewController.h"
#import "FeedbackController.h"
#import "GameViewController.h"
#import "SettingsViewController.h"
#import "HelpViewController.h"
#import "InfoViewController.h"
#import "BaconAppDelegate.h"

@implementation NavigationView

#pragma mark - Properties
@synthesize aboutView = _aboutView;
@synthesize scannerView = _scannerView;
@synthesize mapView = _mapView;
@synthesize feedbackView = _feedbackView;
@synthesize gameView = _gameView;
@synthesize settingsView = _settingsView;
@synthesize helpView = _helpView;
@synthesize infoView = _infoView;

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
	[super viewDidLoad];

	cellContent = [[NSArray arrayWithObjects:
					@"Title",
					@"About",
					@"Scan",
					@"Information",
					@"Map",
					@"Feedback",
					@"Game",
					@"Settings",
					@"Help",
					nil] retain];
	self.navigationItem.title = @"BACON!";
	// Uncomment the following line to preserve selection between presentations.
	// self.clearsSelectionOnViewWillAppear = NO;
 
	// Uncomment the following line to display an Edit button in the navigation bar for this view controller.
	// self.navigationItem.rightBarButtonItem = self.editButtonItem;
}

- (void)viewDidUnload
{
	[super viewDidUnload];
	// Release any retained subviews of the main view.
	// e.g. self.myOutlet = nil;
}

- (void)viewWillAppear:(BOOL)animated
{
	[super viewWillAppear:animated];
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

#pragma mark - Table view data source

- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView
{
#warning Potentially incomplete method implementation.
	// Return the number of sections.
	return 1;
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

/*
// Override to support conditional editing of the table view.
- (BOOL)tableView:(UITableView *)tableView canEditRowAtIndexPath:(NSIndexPath *)indexPath
{
	// Return NO if you do not want the specified item to be editable.
	return YES;
}
*/

/*
// Override to support editing the table view.
- (void)tableView:(UITableView *)tableView commitEditingStyle:(UITableViewCellEditingStyle)editingStyle forRowAtIndexPath:(NSIndexPath *)indexPath
{
	if (editingStyle == UITableViewCellEditingStyleDelete) {
		// Delete the row from the data source
		[tableView deleteRowsAtIndexPaths:[NSArray arrayWithObject:indexPath] withRowAnimation:UITableViewRowAnimationFade];
	}   
	else if (editingStyle == UITableViewCellEditingStyleInsert) {
		// Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view
	}   
}
*/

/*
// Override to support rearranging the table view.
- (void)tableView:(UITableView *)tableView moveRowAtIndexPath:(NSIndexPath *)fromIndexPath toIndexPath:(NSIndexPath *)toIndexPath
{
}
*/

/*
// Override to support conditional rearranging of the table view.
- (BOOL)tableView:(UITableView *)tableView canMoveRowAtIndexPath:(NSIndexPath *)indexPath
{
	// Return NO if you do not want the item to be re-orderable.
	return YES;
}
*/

#pragma mark - Table view delegate
-(void)showSomeView:(ModalViewController *)viewController {
	
	// We are the delegate responsible for dismissing the modal view 
	viewController.delegate = self;
	
	// Create a Navigation controller
	UINavigationController * navController = [[UINavigationController alloc] initWithRootViewController:viewController];
	
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
	[self showCurrentView];
}

//navigation and event handling
- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
	switch (indexPath.row) {
		case 1:
			NSLog(@"You have selected AboutView");
			[self showAboutView];
			break;
		case 2:
			NSLog(@"You have selected scannerView");
			[self showScannerView];
			break;
		case 3:
			NSLog(@"You have selected infoView");
			[self showInfoView];
			break;
		case 4:
			NSLog(@"You have selected mapView");
			[self showMapView];
			break;
		case 5:
			NSLog(@"You have selected FeedbackView");
			[self showFeedbackView];
			break;
		case 6:
			NSLog(@"You have selected the gameView");
			[self showGameView];
			break;
		case 7:
			NSLog(@"You have selected settingsView");
			[self showSettingsView];
			break;
		case 8:
			NSLog(@"You have Selected helpView");
			[self showHelpView];
			break;
		default:
			NSLog(@"You have selected something else");
			exit(-1);
			break;
	}

	// Navigation logic may go here. Create and push another view controller.
   /* if (dvController == nil) {
		Detailviewcontroller *aController =
		[[Detailviewcontroller alloc]initWithNibName:@"DetailView" bundle:nil];
		
		self.dvController = aController;
		
		[aController release];
	}
	[[self navigationController] pushViewController:dvController animated:YES];*/
}

@end
