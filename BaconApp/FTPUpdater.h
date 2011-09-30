//
//  FTPUpdater.h
//  BaconApp
//
//  Created by Donoan Hoffman on 25/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <CFNetwork/CFNetwork.h>

@interface FTPUpdater : NSObject {
	
}
	@property(nonatomic, retain) NSOutputStream		*networkStreamOutput;
	@property(nonatomic, retain) NSInputStream		*networkStreamInput;
	@property(nonatomic, retain) NSOutputStream		*fileWriter;

//+(void) DownloadDirectoryListing;
+(void)DownloadFile:(NSString *)address:(NSString *) filePath;
+(void)DownloadDirectoryListing: (NSString *) address;
//+(void) DownloadFile;

@end
