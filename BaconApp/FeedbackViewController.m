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

@synthesize scrollview, textField1, textField2, textField3, textField4, feedBackTextView;


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
    self.feedBackTextView.returnKeyType = UIReturnKeyDone;
    textField1.returnKeyType = UIReturnKeyDone;
    textField2.returnKeyType = UIReturnKeyDone;
    textField3.returnKeyType = UIReturnKeyDone;
    
    // Hacky method of making a text view look like a text field.
    textField4.frame = CGRectMake(20, 233, 281, 132);
    
    [[NSNotificationCenter defaultCenter] addObserver:self 
                                             selector:@selector(keyboardDidShow:) 
                                                 name:UIKeyboardDidShowNotification 
                                               object:nil];	
    
    self.navigationItem.title = @"Feedback";
      
	[super viewDidLoad];
}
 


/*- (void) viewWillAppear:(BOOL)animated {
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
}*/

-(void) viewWillDisappear:(BOOL)animated {
	//[[NSNotificationCenter defaultCenter] removeObserver:self];
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

-(void)doneButton:(id)sender {
    [textField1 resignFirstResponder];
}

-(BOOL)textViewShouldBeginEditing:(UITextView *)textView
{
    displayAdditionalDoneButton = NO;
    return YES;
}

-(BOOL)textFieldShouldBeginEditing:(UITextField *)textField
{
    if(textField == textField1)
        displayAdditionalDoneButton = YES;
    else
        displayAdditionalDoneButton = NO;
    
    return YES;
}

/*-(void) keyboardDidShow: (NSNotification *)notif {
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
	
}*/

/*-(BOOL) textFieldShouldBeginEditing:(UITextField*)textField {
	Field = textField;
	return YES;
}*/

- (BOOL)textFieldShouldReturn:(UITextField *)textField
{
	[textField resignFirstResponder];
	return YES;
}

-(BOOL) textView:(UITextView *)textView shouldChangeTextInRange:(NSRange)range replacementText:(NSString *)text
{
    if([text isEqualToString:@"\n"])
    {
        [feedBackTextView resignFirstResponder];
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
