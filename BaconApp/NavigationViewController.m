//
//  NavigationView.m
//  BaconApp
//
//  Created by Donovan Hoffman on 14/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "NavigationViewController.h"

@implementation NavigationViewController

#pragma mark - Properties
@synthesize aboutView, scannerView, mapView, feedbackView, gameView, settingsView, helpView, infoView, titleView, updateView, cellContent;

@synthesize currentViewController;

- (void)didDismissModalView {
    // Dismiss the modal view controller
    [self dismissModalViewControllerAnimated:YES];
}


- (void)dealloc
{
    [cellContent dealloc];
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
	cellContent = [[NSArray alloc ] initWithObjects:
					@"Return to Title",
					@"Scan",
					@"Information",
					@"Map",
					@"Feedback",
					@"Game",
					@"Settings",
                    @"About",
                    @"Update",
					@"Help",
					nil];
    
	self.navigationItem.title = @"Menu";

    [self showTitleView];
	//[super viewDidLoad];
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

-(NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section
{
    return [self.cellContent count];
}

- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath
{
    static NSString *CellIdentifier = @"Cell";
    
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:CellIdentifier];
    if (cell == nil)
    {
        cell = [[[UITableViewCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:CellIdentifier] autorelease];
    }
    
    cell.textLabel.text = [cellContent objectAtIndex:[indexPath row]];
    
    return cell;
}

//configure the appearance of the cells
/*- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath
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
}*/


-(void)showSomeView:(ModalViewController *)viewController {
	
	// We are the delegate responsible for dismissing the modal view 
	viewController.delegate = self;
    [self.navigationController pushViewController:viewController animated:YES];
	// Create a Navigation controller
	//UINavigationController * navController = [[UINavigationController alloc] initWithRootViewController:viewController];
    //navController.navigationBar.barStyle = UIBarStyleBlackOpaque;
	
	// show the navigation controller modally
	//[self presentModalViewController:navController animated:YES];
	
	// Clean up resources
	//[navController release];
}

-(void)showCurrentView {
	if (currentViewController) {
		[self showSomeView:self.currentViewController];
	}
}

-(void)showAboutView {
	// Create the modal view controller
	if (!self.aboutView) {
		AboutViewController *viewController = [[AboutViewController alloc] initWithNibName:@"AboutView" bundle:[NSBundle mainBundle]];
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
		ScannerViewController *viewController = [[ScannerViewController alloc] initWithNibName:@"ScannerView" bundle:[NSBundle mainBundle]];
		self.scannerView = viewController;
		currentViewController = viewController;
		// Cleanup resources
		[viewController release];
	}
    else currentViewController = scannerView;
    
	[self showCurrentView];
}
-(void)showInfoView {
	
	BaconAppDelegate * delgato = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	NSArray * scannedCodes = [NSArray arrayWithArray:delgato.scannedItems];
	
	if([scannedCodes count] == 0){
		UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"Alert" message:@"No codes have been scanned, no information will be displayed"
													   delegate:self cancelButtonTitle:@"Ok" otherButtonTitles:nil];
		[alert show];
		[alert release];
	}
	// Create the modal view controller
	if (!self.infoView) {
		InfoViewController *viewController = [[InfoViewController alloc] initWithNibName:@"InfoView" bundle:[NSBundle mainBundle]];
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
		MapViewController *viewController = [[MapViewController alloc] initWithNibName:@"MapView" bundle:[NSBundle mainBundle]];
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
		FeedbackViewController *viewController = [[FeedbackViewController alloc] initWithNibName:@"FeedbackView" bundle:[NSBundle mainBundle]];
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
	BaconAppDelegate * delgato = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	NSArray * scannedCodes = [NSArray arrayWithArray:delgato.scannedItems];
	if([scannedCodes count] == 0){
		UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"Alert" message:@"No codes have been scanned to create the game with"
													   delegate:self cancelButtonTitle:@"Ok" otherButtonTitles:nil];
		[alert show];
		[alert release];
	}
	else{
			if(!self.gameView) {
			GameViewController *viewController = [[GameViewController alloc] initWithNibName:@"GameView" bundle:[NSBundle mainBundle]];
			self.gameView = viewController;
			currentViewController = viewController;
			// Cleanup resources
			[viewController release];
		}
		else currentViewController = gameView;
		[self showCurrentView];
	}
}
-(void)showSettingsView {
	// Create the modal view controller
	if (!self.settingsView) {
		SettingsViewController *viewController = [[SettingsViewController alloc] initWithNibName:@"SettingsView" bundle:[NSBundle mainBundle]];
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
		HelpViewController *viewController = [[HelpViewController alloc] initWithNibName:@"HelpView" bundle:[NSBundle mainBundle]];
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
		TitleViewController *viewController = [[TitleViewController alloc] initWithNibName:@"TitleView" bundle:[NSBundle mainBundle]];
		self.titleView = viewController;
		currentViewController = viewController;
		// Cleanup resources
		[viewController release];
	}
    else currentViewController = titleView;
                     
    [self showCurrentView];
}

-(void)showUpdateView {
	if (!updateView) {
		// Create the modal view controller
		UpdateViewController *viewController = [[UpdateViewController alloc] initWithNibName:@"UpdateView" bundle:[NSBundle mainBundle]];
		self.updateView = viewController;
		currentViewController = viewController;
		// Cleanup resources
		[viewController release];
	}
    else currentViewController = updateView;
    
    [self showCurrentView];
}

//navigation and event handling
- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
	[tableView deselectRowAtIndexPath:indexPath animated:YES];
	
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
            NSLog(@"You have selected UpdateView");
            [self showUpdateView];
            break;
		case 9:
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
