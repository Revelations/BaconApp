//
//  UpdateController.m
//  BaconApp
//
//  Created by Donovan Hoffman on 25/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "UpdateController.h"
#import <CFNetwork/CFNetwork.h>
#import "BaconAppDelegate.h"

@implementation UpdateController

NSOutputStream * _networkStreamO;
NSInputStream * _networkStreamI;
NSOutputStream *  fileStream;

-(IBAction)Update:(id)sender{
    firstLabel.text = @"";
    secondLabel.text = @"Updating.........................";
}
-(IBAction)NoUpdate:(id)sender{
     BaconAppDelegate *appDelegate = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
    appDelegate.update = YES;
    [self dismissModalViewControllerAnimated:YES];
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
    [_networkStreamI release];
    [_networkStreamO release];
    [super dealloc];
}

- (void)didReceiveMemoryWarning
{
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
    
    // Release any cached data, images, etc that aren't in use.
}


- (void)viewDidLoad
{
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
