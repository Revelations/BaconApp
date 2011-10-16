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

-(IBAction)SendFeedback:(id)sender{
	   
	NSString * numbers      = [textFieldNumber text];
	NSString * nationality  = [textFieldNationality text];
	NSString * seen         = [textFieldSighted text];
	NSString * feedback     = [textViewMiscellaneous text];

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
	
	if([updateSession CheckForInternet] != -1){
		[updateSession uploadPhp:filePath];
	}
	else{
		
		//spawns the thread to send feedback
		
		[updateSession spawnThreadForApplication:nil WithPath:filePath WithSleepTime:300 WithType:1];
	}
	[updateSession release];
}

-(IBAction)Cancel:(id)sender
{
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
    [[NSNotificationCenter defaultCenter] removeObserver:self];
	[super dealloc];
}

- (void)didReceiveMemoryWarning
{
	// Releases the view if it doesn't have a superview.
	[super didReceiveMemoryWarning];
	
	// Release any cached data, images, etc that aren't in use.
}


// Implement viewDidLoad to do additional setup after loading the view, typically from a nib.
- (void)viewDidLoad
{
    // Hacky method of making a text view look like a text field.
    textFieldMiscellaneous.frame = CGRectMake(20, 233, 281, 132);
    
    [[NSNotificationCenter defaultCenter] addObserver:self 
                                             selector:@selector(keyboardDidShow:) 
                                                 name:UIKeyboardDidShowNotification 
                                               object:nil];	
    
    self.navigationItem.title = @"Feedback";
      
	[super viewDidLoad];
}

-(void)addButtonToKeyboard
{
	doneButton = [UIButton buttonWithType:UIButtonTypeCustom];
	doneButton.frame = CGRectMake(0, 163, 106, 53);
	doneButton.adjustsImageWhenHighlighted = NO;

    [doneButton setImage:[UIImage imageNamed:@"DoneUp.png"] forState:UIControlStateNormal];
    [doneButton setImage:[UIImage imageNamed:@"DoneDown.png"] forState:UIControlStateHighlighted];
	
    [doneButton addTarget:self action:@selector(doneButton:) forControlEvents:UIControlEventTouchUpInside];
	
    // locate keyboard view
	UIWindow* tempWindow = [[[UIApplication sharedApplication] windows] objectAtIndex:1];
	UIView* keyboard;
	
    for(int i=0; i<[tempWindow.subviews count]; i++)
    {
		keyboard = [tempWindow.subviews objectAtIndex:i];
		// keyboard found, add the button

        if([[keyboard description] hasPrefix:@"<UIPeripheralHost"] == YES)
            [keyboard addSubview:doneButton];
    }
}

-(void) keyboardDidShow: (NSNotification *) note
{
    if(displayAdditionalDoneButton)
        [self addButtonToKeyboard];
}

-(void)doneButton:(id)sender 
{
    [textFieldNumber resignFirstResponder];
}

-(BOOL)textViewShouldBeginEditing:(UITextView *)textView
{
    //displayAdditionalDoneButton = NO;
	scrollView.frame = CGRectMake(0, -200, 320, 455);
    return YES;
}

-(void)textViewDidEndEditing:(UITextView *)textView
{
	scrollView.frame = CGRectMake(0, 0, 320, 455);
}

-(BOOL)textFieldShouldBeginEditing:(UITextField *)textField
{
    if(textField == textFieldNumber)
        displayAdditionalDoneButton = YES;
    
    return YES;
}

-(void)textFieldDidEndEditing:(UITextField *)textField
{
	if(textField == textFieldNumber)
		displayAdditionalDoneButton = NO;
}

- (BOOL)textFieldShouldReturn:(UITextField *)textField
{
	[textField resignFirstResponder];
	return YES;
}

-(BOOL) textView:(UITextView *)textView shouldChangeTextInRange:(NSRange)range replacementText:(NSString *)text
{
    if([text isEqualToString:@"\n"])
    {
        [textViewMiscellaneous resignFirstResponder];
        return NO;
    }
    return YES;
}

- (void)viewDidUnload
{
	[super viewDidUnload];
}

- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation
{
	// Return YES for supported orientations
	return (interfaceOrientation == UIInterfaceOrientationPortrait);
}

@end
