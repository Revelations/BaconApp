//
//  SettingsController.m
//  BaconApp
//
//  Created by Russell Fredericks on 14/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "SettingsController.h"
#import "BaconAppDelegate.h"

@implementation SettingsController

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


// Implement loadView to create a view hierarchy programmatically, without using a nib.
- (void)loadView {
	
}



// Implement viewDidLoad to do additional setup after loading the view, typically from a nib.
- (void)viewDidLoad {
	NSLog(@"Hello, jim and chick norris are loading!");
    [super viewDidLoad];
}

/*
 // Override to allow orientations other than the default portrait orientation.
 - (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation {
 // Return YES for supported orientations.
 return (interfaceOrientation == UIInterfaceOrientationPortrait);
 }
 */
-(IBAction)acceptbutton_click{
	NSString * size = Fontsize.text; 
	NSString * ip = Serverip.text;
	
	NSMutableData *data = [NSMutableData data];
    NSString * delimiter = @"%@|";
    [data appendData:[[NSString stringWithFormat:delimiter,size] dataUsingEncoding:NSUTF8StringEncoding]];    
    [data appendData:[[NSString stringWithFormat:delimiter,ip] dataUsingEncoding:NSUTF8StringEncoding]];
	
	//finding the right directory for the file
	NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
    NSString *documentsDirectory = [paths objectAtIndex:0];
    
    NSString *filePath = [NSString stringWithFormat:@"%@%@", documentsDirectory, @"/Settings.txt"];
    [data writeToFile:filePath atomically:YES];
    [data release];
	BaconAppDelegate * dgate = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
	//BaconAppDelegate *dgate = [UIApplication sharedApplication].delegate;
	dgate.serverIpAddress = ip;
	dgate.fontSize = size;
	[dgate release];
	
};

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
