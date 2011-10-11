//
//  FeedbackController.m
//  BaconApp
//
//  Created by Donovan Hoffman on 12/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "FeedbackController.h"
#import "Update.h"

@implementation FeedbackController


#pragma mark - Actions

-(IBAction)SendFeedback:(id)sender{
    NSString * numbers = [[numberTextField text] autorelease];
    NSString * nationality = [[nationalityTextField text] autorelease];
    NSString * feedback = [[feedBackTextView text] autorelease];
    
    NSMutableData *data = [NSMutableData data];
    NSString * newLine = @"%@\r\n";
    [data appendData:[[NSString stringWithFormat:newLine,numbers] dataUsingEncoding:NSUTF8StringEncoding]];    
    [data appendData:[[NSString stringWithFormat:newLine,nationality] dataUsingEncoding:NSUTF8StringEncoding]];
    [data appendData:[[NSString stringWithFormat:newLine, feedback] dataUsingEncoding:NSUTF8StringEncoding]];
    //NSString * fileContents = [[NSString alloc]initWithData:data encoding:NSUTF8StringEncoding];
    Update * updateSession = [[Update alloc] init];
    
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
    NSString *documentsDirectory = [paths objectAtIndex:0];
    
 //   NSArray *values = [urlPath componentsSeparatedByString:@"/"];
//    NSString *backend = [NSString stringWithFormat:@"%@%@", @"/", [values objectAtIndex:[values count] -1]];
    
    
    NSString *filePath = [NSString stringWithFormat:@"%@%@", documentsDirectory, @"/feedback.txt"];
    [data writeToFile:filePath atomically:YES];
    [data release];
    
    [updateSession uploadPhp:filePath];
    [updateSession release];
}

-(IBAction)Cancel:(id)sender{
    NSLog(@"Carry on Jim");
    //BaconAppDelegate *appDelegate = (BaconAppDelegate *)[[UIApplication sharedApplication] delegate];
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
    [super dealloc];
}

- (void)didReceiveMemoryWarning
{
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
    
    // Release any cached data, images, etc that aren't in use.
}

#pragma mark - View lifecycle

/*
// Implement loadView to create a view hierarchy programmatically, without using a nib.
- (void)loadView
{
}
*/

/*
// Implement viewDidLoad to do additional setup after loading the view, typically from a nib.
- (void)viewDidLoad
{
    [super viewDidLoad];
}
*/

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
