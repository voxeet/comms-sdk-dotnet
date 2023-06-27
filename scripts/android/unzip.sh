SCRIPT_DIR=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )

VERSION=$1
ARCH=$2
AAR=$3


if [[ -z "$VERSION" ]]; then
  VERSION="2.5.3"
fi

if [[ -z "$ARCH" ]]; then
  ARCH="arm64-v8a"
fi

AAR_OUTPUT="$SCRIPT_DIR/../../build/aar"
RELEASE="$SCRIPT_DIR/../../build/sdk-release-android/$ARCH"

echo "using Android version 2.5.3"

if [[ -z "$AAR" ]]; then
  echo "using default local maven"
  AAR=~/.m2/repository/io/dolby/comms-sdk-android-cppsdk/$VERSION/*.aar
fi

rm -rf $AAR_OUTPUT || echo "skipping cleaning .tmp/aar"
mkdir -p $AAR_OUTPUT || echo "tmp already exists, skipping"

# extract all the files we will need for compilation only ...
pushd $AAR_OUTPUT
unzip "$AAR"
popd

# now copy the files
# Note : we are using root/sdk-release-android but a sdk-release/$platform would be preferrable
rm -rf $RELEASE
mkdir $RELEASE

mkdir -p $RELEASE/libs/
mkdir -p $RELEASE/include/

# copy the shared objects
cp -r $AAR_OUTPUT/prefab/modules/sdk/include/* $RELEASE/include
cp -r $AAR_OUTPUT/prefab/modules/sdk/libs/android.$ARCH/libdolbyio_comms_sdk.so $RELEASE/libs
cp -r $AAR_OUTPUT/jni/$ARCH/libdolbyio_comms_media.so $RELEASE/libs
cp -r $AAR_OUTPUT/jni/$ARCH/libdolbyio_comms_sdk_android_cppsdk.so $RELEASE/libs
cp -r $AAR_OUTPUT/jni/$ARCH/libdvclient.so $RELEASE/libs
cp -r $AAR_OUTPUT/jni/$ARCH/libdvdnr.so $RELEASE/libs
