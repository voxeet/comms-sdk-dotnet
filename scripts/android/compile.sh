# export NDK_ROOT=/path/to/android/sdk/ndk/25.1.893739
SCRIPT_DIR=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )

ARCH=$2

if [[ -z "$ARCH" ]]; then
  ARCH="arm64-v8a"
fi

INSTALL=$SCRIPT_DIR/../../build/install
mkdir -p $INSTALL

cmake \
    -DCMAKE_SYSTEM_NAME=Android \
    -DCMAKE_EXPORT_COMPILE_COMMANDS=ON \
    -DCMAKE_SYSTEM_VERSION=23 \
    -DANDROID_PLATFORM=android-23 \
    -DANDROID_ABI="$ARCH" \
    -DCMAKE_ANDROID_ARCH_ABI="$ARCH" \
    -DANDROID_NDK="$NDK_ROOT" \
    -DCMAKE_ANDROID_NDK="$NDK_ROOT" \
    -DCMAKE_TOOLCHAIN_FILE="$NDK_ROOT/build/cmake/android.toolchain.cmake" \
    "$SCRIPT_DIR/../.." \
    -B"$INSTALL" \
    -DANDROID=ON \
    -DANDROID_STL=c++_shared \
    -DDOLBYIO_LIBRARY_PATH="$SCRIPT_DIR/../../build/sdk-release-android/$ARCH"

cd $INSTALL

cmake --build .