//
//  FeedbackController.m
//  BaconApp
//
//  Created by Donovan Hoffman on 12/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "FeedbackViewController.h"
#import "Update.h"
#import "Reachability.h"
#import "BaconAppDelegate.h"

@implementation FeedbackViewController

#pragma mark - Actions

@synthesize scrollview, textField1, textField2, textField3, textField4;


-(IBAction)SendFeedback:(id)sender{
	   
	NSString * numbers      = [[numberTextField text] autorelease];
	NSString * nationality  = [[nationalityTextField text] autorelease];
	NSString * feedback     = [[feedBackTextView text] autorelease];
	NSString * seen         = [[seenTextField text] autorelease];
		NSMutableArray * scannedContents = [(BaconAppDelegate *) [[UIApplication sharedApplication] delegate]scannedItems];
	int val_count = [scannedContents count];
	
	NSMutableData *data = [NSMutableData data];
	NSString * newLine = @"%@\r\n";
	[data appendData:[[NSString stringWithFormat:newLine, numbers] dataUsingEncoding:NSUTF8StringEncoding]];    
	[data appendData:[[NSString stringWithFormat:newLine, nationality] dataUsingEncoding:NSUTF8StringEncoding]];
	[data appendData:[[NSString stringWithFormat:newLine, seen] dataUsingEncoding:NSUTF8StringEncoding]];
	[data appendData:[[NSString stringWithFormat:newLine, feedback] dataUsingEncoding:NSUTF8StringEncoding]];
	[data appendData:[[NSString stringWithFormat:newLine, [NSString stringWithFormat:@"%d", val_count]] dataUsingEncoding:NSUTF8StringEncoding]];
	Update * updateSession = [[Update alloc] init];
	
	NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
	NSString *documentsDirectory = [paths objectAtIndex:0];
	
	NSString *filePath = [NSString stringWithFormat:@"%@%@", documentsDirectory, @"/feedback.txt"];
	[data writeToFile:filePath atomically:YES];
	[data release];
	
	if([updateSession CheckForInternet] != -1){
		[updateSession uploadPhp:filePath];
	}
	else{
		
		//spawns the thread to send feedback
		
		[updateSession spawnThreadForApplication:nil WithPath:filePath WithSleepTime:300 WithType:1];
	}
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


// Implement viewDidLoad to do additional setup after loading the view, typically from a nib.
- (void)viewDidLoad
{
    self.navigationItem.title = @"Feedback";
    
	[super viewDidLoad];
}
 


- (void) viewWillAppear:(BOOL)animated {
	[super viewWillAppear:animated];
	
	[[NSNotificationCenter defaultCenter] 
	 addObserver:self
	 selector:@selector
	 (keyboardDidShow:) 
	 name: UIKeyboardDidShowNotification
	 object:nil];
	[[NSNotificationCenter defaultCenter]
	 addObserver:self 
	 selector:@selector
	 (keyboardDidHide:) name:
	 UIKeyboardDidHideNotification
	 object:nil];
	
	scrollview.contentSize = CGSizeMake(320,
										460);
	
	displayKeyboard = NO;
}

-(void) viewWillDisappear:(BOOL)animated {
	[[NSNotificationCenter defaultCenter]
	 removeObserver:self];
}

-(void) keyboardDidShow: (NSNotification *)notif {
	if (displayKeyboard) {
		return;
	}
	
	NSDictionary* info = [notif userInfo];
	NSValue* aValue = [info objectForKey:UIKeyboardBoundsUserInfoKey];
	CGSize keyboardSize = [aValue CGRectValue].size;
	
	offset = scrollview.contentOffset;
	
	CGRect viewFrame = scrollview.frame;
	viewFrame.size.height -= keyboardSize.height;
	scrollview.frame = viewFrame;
	
	CGRect textFieldRect = [Field frame];
	textFieldRect.origin.y += 10;
	[scrollview scrollRectToVisible: textFieldRect animated:YES];
	displayKeyboard = YES;
}

-(void) keyboardDidHide: (NSNotification *)notif {
	if (!displayKeyboard) {
		return; 
	}
	
	scrollview.frame = CGRectMake(0, 0, 320,460);
	
	scrollview.contentOffset =offset;
	
	displayKeyboard = NO;
	
}

-(BOOL) textFieldShouldBeginEditing:(UITextField*)textField {
	Field = textField;
	return YES;
}

- (BOOL)textFieldShouldReturn:(UITextField *)textField
{
	[textField resignFirstResponder];
	return YES;
}



- (void) touchesBegan: (NSSet *) touches withEvent: (UIEvent *) event {  
    if (textField1) {  
        if ([textField1 canResignFirstResponder]) [textField1 resignFirstResponder];  
    }  
    if (textField2) {  
        if ([textField2 canResignFirstResponder]) [textField2 resignFirstResponder];  
    }
	if (textField3) {  
        if ([textField3 canResignFirstResponder]) [textField3 resignFirstResponder];  
    }
	if (textField4) {  
        if ([textField4 canResignFirstResponder]) [textField4 resignFirstResponder];  
    }
	if (numberTextField) {  
        if ([numberTextField canResignFirstResponder]) [numberTextField resignFirstResponder];  
    }
	if (feedBackTextView) {  
        if ([feedBackTextView canResignFirstResponder]) [feedBackTextView resignFirstResponder];  
    }  
    if (seenTextField) {  
        if ([seenTextField canResignFirstResponder]) [seenTextField resignFirstResponder];  
    }  
    if (nationalityTextField) {  
        if ([nationalityTextField canResignFirstResponder]) [nationalityTextField resignFirstResponder];  
    }  
    [super touchesBegan: touches withEvent: event];  
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
