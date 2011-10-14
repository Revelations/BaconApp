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
    cellContent = [[NSArray arrayWithObjects:@"Title", @"About", @"Scan",@"Information", @"Map"
    , @"Feedback", @"Game", @"Settings", @"Help", nil] retain];
    [super viewDidLoad];

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
    return 0;
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
    cell.text = [cellContent objectAtIndex:indexPath.row];
    
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


//customize the appearance of the table cells
- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
    switch (indexPath.row) {
        case 1:
            if(aboutView == nil){
                AboutViewController * aController =
                [[AboutViewController alloc]initWithNibName:@"AboutView" bundle:nil];
                self.aboutView = aController;
                [aController release];
            }
            [[self navigationController] pushViewController:aboutView animated:YES];
            break;
        case 2:
            if(scannerView == nil){
                ScannerViewController * aController = 
                [[ScannerViewController alloc] initWithNibName:@"ScannerView" bundle:nil];
                self.scannerView = aController;
                [aController release];
            }
            [[self navigationController]  pushViewController:scannerView animated:YES];
            break;
        case 3:
            if(infoView == nil){
                InfoViewController * aController =
                [[InfoViewController alloc] initWithNibName:@"InfoView" bundle:nil];
                self.infoView = aController;
                [aController release];
            }
            [[self navigationController]  pushViewController:infoView animated:YES];
            break;
        case 4:
            if(mapView == nil){
                MapViewController * aController =
                [[MapViewController alloc] initWithNibName:@"MapView" bundle:nil];
                self.mapView = aController;
                [aController release];
            }
            [[self navigationController]  pushViewController:mapView animated:YES];
            break;
            
        case 5:
            if(feedbackView == nil){
                FeedbackController * aController =
                [[FeedbackController alloc] initWithNibName:@"FeedbackView" bundle:nil];
                self.feedbackView = aController;
                [aController release];
            }
            [[self navigationController]  pushViewController:feedbackView animated:YES];
            break;
            
        case 6:
            if(gameView == nil){
                GameViewController * aController =
                [[GameViewController alloc] initWithNibName:@"GameView" bundle:nil];
                self.gameView = aController;
                [aController release];
            }
            [[self navigationController]  pushViewController:gameView animated:YES];
            break;
            
        case 7:
            if(settingsView == nil){
                SettingsViewController * aController =
                [[SettingsViewController alloc] initWithNibName:@"SettingsView" bundle:nil];
                self.settingsView = aController;
                [aController release];
            }
            [[self navigationController]  pushViewController:settingsView animated:YES];
            break;
            
        case 8:
            if(helpView == nil){
                HelpViewController * aController =
                [[HelpViewController alloc] initWithNibName:@"helpView" bundle:nil];
                self.infoView = aController;
                [aController release];
            }
            [[self navigationController]  pushViewController:helpView animated:YES];
            break;
        default:
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
