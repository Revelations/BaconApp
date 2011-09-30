//
//  UpdateController.m
//  BaconApp
//
//  Created by Donovan Hoffman on 1/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "UpdateController.h"
#import "Update.h"

@implementation UpdateController


-(IBAction)Update:(id)sender{
    Update *currentFileRequest = [[Update alloc] init];
    [currentFileRequest getFile:@"http://revelations.webhop.org:81/kiwi.html"];
	/*FTPDirectory *ftpDir = [[FTPDirectory alloc] init];//alloca(sizeof(FTPDirectory));
	FTPFile *ftpFile = [[FTPFile alloc] init];
	NSLog(@"hello jello");
    
	[ftpDir startReceive:@"ftp://revelations.webhop.org"];
	//[ftpFile DownloadFile:@"ftp://reveleations.webhop.org/New File.html" :@"~test.txt"];	
	NSLog(@"file at 0 is : %@ " ,ftpDir.listData[0]);
	NSLog(@"hello jello");
	
	textInfo.text = @"Updating..........";*/
    
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
