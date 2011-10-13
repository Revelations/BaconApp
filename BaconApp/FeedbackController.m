//
//  FeedbackController.m
//  BaconApp
//
//  Created by Donovan Hoffman on 12/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "FeedbackController.h"
#import "Update.h"
#import "Reachability.h"
#import "BaconAppDelegate.h"

@implementation FeedbackController


#pragma mark - Actions



-(IBAction)SendFeedback:(id)sender{
       
    NSString * numbers      = [[numberTextField text] autorelease];
    NSString * nationality  = [[nationalityTextField text] autorelease];
    NSString * feedback     = [[feedBackTextView text] autorelease];
    NSString * seen         = [[seenTextField text] autorelease];
        NSMutableArray * scannedContents = [(BaconAppDelegate *) [[UIApplication sharedApplication] delegate]scannedItems];
    int val_count = [scannedContents count];
    
    NSMutableData *data = [NSMutableData data];
    NSString * newLine = @"%@\r\n";
    [data appendData:[[NSString stringWithFormat:newLine,numbers] dataUsingEncoding:NSUTF8StringEncoding]];    
    [data appendData:[[NSString stringWithFormat:newLine,nationality] dataUsingEncoding:NSUTF8StringEncoding]];
    [data appendData:[[NSString stringWithFormat:newLine,seen] dataUsingEncoding:NSUTF8StringEncoding]];
    [data appendData:[[NSString stringWithFormat:newLine, feedback] dataUsingEncoding:NSUTF8StringEncoding]];
    [data appendData:[[NSString stringWithFormat:newLine, [NSString stringWithFormat:@"%d", val_count]] dataUsingEncoding:NSUTF8StringEncoding]];
    Update * updateSession = [[Update alloc] init];
    
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
    NSString *documentsDirectory = [paths objectAtIndex:0];
    
    NSString *filePath = [NSString stringWithFormat:@"%@%@", documentsDirectory, @"/feedback.txt"];
    [data writeToFile:filePath atomically:YES];
    [data release];
    
    
    Reachability  * receptionCheck = [[Reachability alloc]init];
    
    if([updateSession CheckForInternet: receptionCheck] != -1){
        [updateSession uploadPhp:filePath];
    }
    else{
        
        //spawns the thread to send feedback
        dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, NULL), ^{
            while (YES) {
                if([updateSession CheckForInternet:receptionCheck] != -1)
                    break;
                else
                    sleep(300);
            }
            [updateSession uploadPhp:filePath];
        });
    }
    
    [updateSession uploadPhp:filePath];
    
    [receptionCheck release];
    [updateSession release];
}


//returns -1 if no connection is possible
//returns 1 if wwan is available
//returns 0 if wifi is available


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
