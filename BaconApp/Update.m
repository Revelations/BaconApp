	//
//  Update.m
//  BaconApp
//
//  Created by Donovan Hoffman on 1/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import "Update.h"
#import "Reachability.h"


@implementation Update

@synthesize isSendingU;
@synthesize networkStreamU;
@synthesize fileStreamU;
@synthesize bufferU;
@synthesize bufferOffsetU;
@synthesize bufferLimitU;




    
-(int)CheckForInternet: (Reachability *) curReach{
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


-(void)uploadPhp:(NSString *) filePath{
        
    NSLog(@"I am in in the upload php method");
    NSLog(@"%@", filePath);
    NSString *urlString = @"http://revelations.webhop.org:81/uploadFile.php";
    NSData *data = [NSData dataWithContentsOfFile:filePath ];
        
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

}

-(void)getFile:(NSString *)uP :(NSString *) filePath{


    NSString * urlPath = [uP stringByReplacingOccurrencesOfString:@" "withString:@"%20"];
        NSLog(@"File is being downloaded from %@ and being written to %@",urlPath,filePath);
    NSURL *url = [NSURL URLWithString:urlPath];
    
    NSError * err = [[NSError alloc] init];    
    //retrieves the data from the url --Donovan
    NSData *urlData = [NSData dataWithContentsOfURL:url options:NSDataReadingUncached error:&err];
    
  //  if (err) {
  //      NSLog(@"Error : %@", [err localizedDescription]);
  //      [err release];
  //  } else {
  //      NSLog(@"Data has loaded successfully.");
  //  }
      
    //checks to see if the urlData has been downloaded
    if(urlData){
        NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
        //NSString *documentsDirectory = [paths objectAtIndex:0];
        
        //NSArray *values = [urlPath componentsSeparatedByString:@"/"];
        //NSString *backend = [NSString stringWithFormat:@"%@%@", @"/", [values objectAtIndex:[values count] -1]];
        
        
        //NSString *filePath = [NSString stringWithFormat:@"%@%@", documentsDirectory, backend];
        [urlData writeToFile:filePath atomically:YES];
    }	
        else{
            NSLog(@"%@ File Was not created", filePath);
        }
    }


-(void)getDirectory:(NSString *)urlPath{
    
    NSURL *url = [NSURL URLWithString:[NSString stringWithFormat: @"%@%@", urlPath, @"log.txt"]];
    
    //retrieves the data from the url --Donovan
    NSData *urlData = [NSData dataWithContentsOfURL:url];
    
    if(urlData){
        
        //finds the relevant directory to ensure that iOS does not purge it --Donovan
        NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
        NSString *documentsDirectory = [paths objectAtIndex:0];
        
        NSString *filePath = [NSString stringWithFormat:@"%@%@", documentsDirectory, @"//log.txt"];
        [urlData writeToFile:filePath atomically:YES];
        
        NSString *fileContents = [NSString stringWithContentsOfFile:filePath encoding:NSUTF8StringEncoding error:nil];
        
        NSArray *values = [fileContents componentsSeparatedByString:@"|"];
                
        
        //iterates over the files it needs to download
        for(int i = 0; i < [values count]; i++){
            NSString *s = [values objectAtIndex:i];
            NSString *filePath = [NSString stringWithFormat:@"%@%@%@", documentsDirectory,@"/", s];
            NSFileManager *fileManager = [NSFileManager defaultManager];
            NSString *retrieveUrl = [NSString stringWithFormat:@"%@%@", urlPath,s]; 
            if ([fileManager fileExistsAtPath:filePath]) {
                
                NSDictionary *attrs = [fileManager attributesOfItemAtPath: filePath error: NULL];
                
                int result = [attrs fileSize];                
                int size = [[values objectAtIndex:++i] intValue];
                
                //checks to see if it already has the file
                if(size != result){	
                    [self getFile:retrieveUrl:filePath];
                }
            }
            else{
                i++;
                [self getFile:retrieveUrl:filePath];
            }
        }
        }
        else{
            NSLog(@"Directory was not created");
        }    
}

-(void)GetGameFiles:(NSString *) urlPath{
    NSURL *url = [NSURL URLWithString:[NSString stringWithFormat: @"%@%@", urlPath, @"Game/gamelog.txt"]];
    NSError * err = [[NSError alloc] init];  
    NSLog(@"Downloading dir info from %@", url);
    //retrieves the data from the url --Donovan     
    NSData *urlData = [NSData dataWithContentsOfURL:url];
    
   
    
    if(urlData){        
        
        //finds the relevant directory to ensure that iOS does not purge it --Donovan
        NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
        NSString *documentsDirectory = [paths objectAtIndex:0];
        
        NSFileManager *fileManager = [NSFileManager defaultManager];
        [fileManager createDirectoryAtPath:[NSString stringWithFormat: @"%@%@",documentsDirectory, @"/Game"] withIntermediateDirectories:NO attributes:nil error: nil];
        NSString *filePath = [NSString stringWithFormat:@"%@%@", documentsDirectory, @"/Game/log.txt"];
        NSLog(@"log is being written to %@", filePath);
        [urlData writeToFile:filePath atomically:YES];
        
        NSString *fileContents = [NSString stringWithContentsOfFile:filePath encoding:NSUTF8StringEncoding error:nil];
        
        NSArray *values = [fileContents componentsSeparatedByString:@"|"];
        
        
        //iterates over the files it needs to download
        for(int i = 0; i < [values count]; i++){
            NSString *s = [values objectAtIndex:i];
            NSString *filePath = [NSString stringWithFormat:@"%@%@%@", documentsDirectory,@"/Game/", s];
//            NSFileManager *fileManager = [NSFileManager defaultManager];
            NSString *retrieveUrl = [NSString stringWithFormat:@"%@Game/%@", urlPath,s]; 
            NSLog(@"retrieveURL has value of %@", retrieveUrl);
            if ([fileManager fileExistsAtPath:filePath]) {
                
                NSDictionary *attrs = [fileManager attributesOfItemAtPath: filePath error: NULL];
                
                int result = [attrs fileSize];                
                int size = [[values objectAtIndex:++i] intValue];
                
                //checks to see if it already has the file
                if(size != result){	
                    [self getFile:retrieveUrl:filePath];
                }
            }
            else{
                i++;
                [self getFile:retrieveUrl:filePath];
            }
        }
    }
    else{
        NSLog(@"Directory was not created");
    }    

}

@end

