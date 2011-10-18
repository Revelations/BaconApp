//
//  Update.m
//  BaconApp
//
//  Created by Donovan Hoffman on 1/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "Update.h"
#import "Reachability.h"
#import "BaconAppDelegate.h"

@implementation Update

@synthesize isSendingU;
@synthesize networkStreamU;
@synthesize fileStreamU;
@synthesize bufferU;
@synthesize bufferOffsetU;
@synthesize bufferLimitU;

// Shii's idea for really long naming scheme! :D -- Shii
-(void) spawnThreadForApplication:(UIApplication *)application WithPath:(NSString *)filePath WithSleepTime:(int)sleepTime WithType:(int)type{
	if (type) { // Foreground
		dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, (int)NULL), ^{
			while (YES) {
				if([self CheckForInternet] != -1)
					break;
				else
					sleep(sleepTime);
			}
			[self uploadPhp:filePath];
		});
	}
	else { // Background
		dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, (int)NULL), ^{
			while ([application backgroundTimeRemaining] > 5.0) {
				if([self CheckForInternet] != -1)
					break;
				else
					sleep(sleepTime);
			}
			[self uploadPhp:filePath];
		});
	}	
}

-(int)CheckForInternet{
//	Reachability reachabili
//	[kReachabilityChangedNotification object: noteObject];
	Reachability * curReach = [Reachability reachabilityForInternetConnection];
	NetworkStatus netStatus = [curReach currentReachabilityStatus];
	switch (netStatus)
	{
		case NotReachable:
		{
			return -1;
		}
			
		case ReachableViaWWAN:
		{
			return 1;
		}
		case ReachableViaWiFi:
		{
			return 0;
		}
	}
	return -1;
}


-(void)uploadPhp:(NSString *) filePath {
	
	NSLog(@"I am in in the upload php method");
	NSLog(@"%@", filePath);
	NSString *urlString = @"http://revelations.webhop.org:81/uploadFile.php";
	NSData *data = [NSData dataWithContentsOfFile:filePath];
	
	NSLog(@"I am about to create the request");
	//the request object
	NSMutableURLRequest *request = [[[NSMutableURLRequest alloc] init] autorelease];
	[request setURL:[NSURL URLWithString:urlString]];
	[request setHTTPMethod:@"POST"];
	
	NSLog(@"Defining the vars");
	NSString *boundary = [NSString stringWithString:@"---------------------------14737809831466499882746641449"];
	NSString *contentType = [NSString stringWithFormat:@"multipart/form-data; boundary=%@",boundary];
	[request addValue:contentType forHTTPHeaderField: @"Content-Type"];
	

	/*the body of the post */
	NSLog(@"creating the body of the request");
	NSString * feedbackFilename = [NSString stringWithFormat:@"%f%@", [[NSDate date] timeIntervalSince1970], @"feedback"];
	
	NSString *content = [NSString stringWithFormat:@"Content-Disposition: form-data; name=\"userfile\"; filename=\"%@.fbk\"\r\n",feedbackFilename];
	
	NSMutableData *body = [NSMutableData data];
	[body appendData:[[NSString stringWithFormat:@"\r\n--%@\r\n",boundary] dataUsingEncoding:NSUTF8StringEncoding]];    
	[body appendData:[[NSString stringWithString:content] dataUsingEncoding:NSUTF8StringEncoding]];
	[body appendData:[[NSString stringWithString:@"Content-Type: text/plain\r\n\r\n"] dataUsingEncoding:NSUTF8StringEncoding]];
	[body appendData:[NSData dataWithData:data]];
	[body appendData:[[NSString stringWithFormat:@"\r\n--%@--\r\n",boundary] dataUsingEncoding:NSUTF8StringEncoding]];
	
	[request setHTTPBody:body];
	
	// now lets make the connection to the web
	
	NSLog(@"sending the request");
	NSData *returnData = [NSURLConnection sendSynchronousRequest:request returningResponse:nil error:nil]; 
	if(returnData){
		[returnData writeToFile:@"result.txt" atomically:YES];
		NSLog(@"success!");
	}
	else{
		NSLog(@"failure!");
	}
	NSFileManager *fileManager = [NSFileManager defaultManager];
	if([fileManager isDeletableFileAtPath:filePath]){
		[fileManager removeItemAtPath:filePath error: NULL];
	}
	
}
#pragma mark - Directories
// finds the relevant directory to ensure that iOS does not purge it --Donovan
+(NSString *)unpurgableDirectory { // Refactored by Shii
	NSArray * paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES); 
	return [paths objectAtIndex:0];
}

#pragma mark - Remote Methods
+(NSString *) remoteFeedbackDir:(NSString *) urlPath{
		return @"";//[NSURL URLWithString:@"%@/%@" @"ftp://",urlPath
}

+(NSString *) remoteContentDir:(NSString *)urlPath {
	return [NSString stringWithFormat:@"%@/%@", urlPath, CONTENT_DIRECTORY];
}
+(NSString *) remoteGameDir:(NSString *)urlPath {
	return [NSString stringWithFormat:@"%@/%@", urlPath, GAME_DIRECTORY];
}
#pragma mark - Log Files URLs
+(NSURL    *) remoteContentLogURL:(NSString *)urlPath {
	return [NSURL URLWithString:[NSString stringWithFormat: @"%@/%@", [Update remoteContentDir:urlPath], @"log.txt"]];
}
+(NSURL    *) remoteGameLogURL:(NSString *)urlPath {
	return [NSURL URLWithString:[NSString stringWithFormat: @"%@/%@", [Update remoteGameDir:urlPath], @"gamelog.txt"]];
}

#pragma mark - Local Methods
+(NSString *) localContentDir {
	return [NSString stringWithFormat:@"%@/%@", [Update unpurgableDirectory], CONTENT_DIRECTORY];
}
+(NSString *) localGameDir {
	return [NSString stringWithFormat:@"%@/%@", [Update unpurgableDirectory], GAME_DIRECTORY];
}
+(NSString *) localContentLogFile {
	return [NSString stringWithFormat:@"%@/%@", [Update localContentDir], @"log.txt"];
}
+(NSString *) localGameLogFile {
	return [NSString stringWithFormat:@"%@/%@", [Update localGameDir], @"gamelog.txt"];
}


-(NSData *) dataForContentFromPath:(NSString *) urlPath {
	return [NSData dataWithContentsOfURL: [Update remoteContentLogURL:urlPath]];
}
-(NSData *) dataForGameFromPath:(NSString *) urlPath {
	//retrieves the data from the url --Donovan
	return [NSData dataWithContentsOfURL: [Update remoteGameLogURL:urlPath]];
}

-(void)getFile:(NSString *)urlPath:(NSString *)filePath {
	//Replace spaces with %20
	NSString * parsedUrlPath = [urlPath stringByReplacingOccurrencesOfString:@" " withString:@"%20"];
	NSLog(@"File downloading from %@ to %@", parsedUrlPath, filePath);
	NSURL *url = [NSURL URLWithString:parsedUrlPath];
	
	NSError * err;    
	//retrieves the data from the url --Donovan
	NSData *urlData = [NSData dataWithContentsOfURL:url options:NSDataReadingUncached error:&err];
	
	  /*if (err) {
	      NSLog(@"Error : %@", [err localizedDescription]);
	      [err release];
	  } else {
	      NSLog(@"Data has loaded successfully.");
	  }*/
	
	//checks to see if the urlData has been downloaded
	if(urlData){
		//NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
		//NSString *documentsDirectory = [paths objectAtIndex:0];
		
		//NSArray *values = [urlPath componentsSeparatedByString:@"/"];
		//NSString *backend = [NSString stringWithFormat:@"%@%@", @"/", [values objectAtIndex:[values count] -1]];
		
		//NSString *filePath = [NSString stringWithFormat:@"%@%@", documentsDirectory, backend];
		NSLog(@"WRITNG TO FILE; %@", filePath);
		[urlData writeToFile:filePath atomically:YES];
	}	
	else{
		NSLog(@"%@ was not created", filePath);
	}
}

-(void)downloadContentFilesFrom:(NSString *)urlPath {
	//retrieves the data from the url --Donovan
	NSData * urlData = [self dataForContentFromPath:urlPath];
	if(urlData) {
		
		NSFileManager *fileManager = [NSFileManager defaultManager];
		[fileManager createDirectoryAtPath:[Update localContentDir] withIntermediateDirectories:NO attributes:nil error:nil];
		NSLog(@"log is being written to %@", [Update localContentLogFile]);
		[urlData writeToFile:[Update localContentLogFile] atomically:YES];
		
		NSString *fileContents = [NSString stringWithContentsOfFile:[Update localContentLogFile] encoding:NSUTF8StringEncoding error:nil];
		
		NSArray *values = [fileContents componentsSeparatedByString:@"|"];
		
		//iterates over the files it needs to download
		for(int i = 0; i < [values count]; i++) {
			NSString *s = [values objectAtIndex:i];
			NSString *itemPath = [NSString stringWithFormat:@"%@/%@", [Update localContentDir], s];
			NSString *retrievePath = [NSString stringWithFormat:@"%@/%@", [Update remoteContentDir:urlPath], s];
			NSLog(@"retrieveURL has value of %@", retrievePath);
			NSLog(@"Item path is %@", itemPath);
			if ([fileManager fileExistsAtPath:itemPath]) {
				NSLog(@"File exists");
				
				NSDictionary *attrs = [fileManager attributesOfItemAtPath:itemPath error:NULL];
				
				int result = [attrs fileSize];
				int size = [[values objectAtIndex:++i] intValue];
				
				//checks to see if it already has the file
				if(size != result) {
					[self getFile:retrievePath:itemPath];
				}
			}
			else {
				i++;
				[self getFile:retrievePath:itemPath];
			}
		}
	}
	else{
		NSLog(@"Directory was not created");
	}    
}

// urlPath does not need slash at the end.
// Currently, revelations.webhop.org
-(void)GetGameFiles:(NSString *) urlPath {
	NSLog(@"-(void)GetGameFiles:(NSString *) urlPath;");
	NSData *data = [self dataForGameFromPath:urlPath];
	if (data) {
		NSFileManager *fileManager = [NSFileManager defaultManager];
		[fileManager createDirectoryAtPath:[Update localGameDir]
			   withIntermediateDirectories:NO
								attributes:nil
									 error:nil];
		NSLog(@"log is being written to %@", [Update localGameLogFile]);
		[data writeToFile:[Update localGameLogFile]
			   atomically:YES];
		
		NSString *fileContents = [NSString stringWithContentsOfFile:[Update localGameLogFile] encoding:NSUTF8StringEncoding error:nil];
		
		NSArray *values = [fileContents componentsSeparatedByString:@"|"];
		
		//iterates over the files it needs to download
		for(int i = 0; i < [values count]; i++){
			NSString *s = [values objectAtIndex:i];
			NSString *itemPath = [NSString stringWithFormat:@"%@/%@", [Update localGameDir], s];
			// NSFileManager *fileManager = [NSFileManager defaultManager];
			NSString *retrieveUrl = [NSString stringWithFormat:@"%@/%@", [Update remoteGameDir:urlPath], s]; 
			NSLog(@"retrieveURL has value of %@", retrieveUrl);

			NSLog(@"kog maw is back, looking at %@",itemPath);
			if ([fileManager fileExistsAtPath:itemPath]) {
				NSDictionary *attrs = [fileManager attributesOfItemAtPath:itemPath error:NULL];
				
				int result = [attrs fileSize];                
				int size = [[values objectAtIndex:++i] intValue];
				
				//checks to see if it already has the file
				if(size != result){	
					[self getFile:retrieveUrl:itemPath];
					NSLog(@"file is downloading : %@", itemPath);
				}
				else {
					NSLog(@"File is not downloading, wrong size");
				}
			}
			else {
				NSLog(@"Downloading file: %@", itemPath);
				i++;
				[self getFile:retrieveUrl:itemPath];
			}
		}
	}
	else{
		NSLog(@"Directory was not created");
	}    
	
}

@end

