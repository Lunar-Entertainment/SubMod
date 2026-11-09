#!/usr/bin/env bash
# ==============================================================================
# Subnautica: Call of the Abyss - Automated Zorin OS / Linux Installer
# Installs BepInEx, Nautilus, and CallOfTheAbyss directly into Subnautica.
# ==============================================================================

set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
BUNDLE_DIR="$SCRIPT_DIR/dist/Subnautica_CallOfTheAbyss_CompleteBundle"

echo "=========================================================="
echo "  🌊 Subnautica: Call of the Abyss - Zorin OS Installer   "
echo "=========================================================="
echo ""

# Common Subnautica locations on Zorin OS / Linux
CANDIDATE_PATHS=(
    "$HOME/.steam/steam/steamapps/common/Subnautica"
    "$HOME/.local/share/Steam/steamapps/common/Subnautica"
    "$HOME/.var/app/com.valvesoftware.Steam/.steam/steam/steamapps/common/Subnautica"
    "$HOME/.var/app/com.valvesoftware.Steam/.local/share/Steam/steamapps/common/Subnautica"
)

TARGET_DIR=""

# Check passed argument
if [ -n "$1" ] && [ -d "$1" ]; then
    TARGET_DIR="$1"
else
    # Auto-detect location
    for p in "${CANDIDATE_PATHS[@]}"; do
        if [ -d "$p" ]; then
            TARGET_DIR="$p"
            break
        fi
    done
fi

if [ -z "$TARGET_DIR" ]; then
    echo "⚠️  Could not automatically detect your Subnautica directory."
    echo ""
    read -rp "Please enter the absolute path to your Subnautica game folder: " USER_PATH
    if [ -d "$USER_PATH" ]; then
        TARGET_DIR="$USER_PATH"
    else
        echo "❌ Directory not found: $USER_PATH"
        exit 1
    fi
fi

echo "✅ Found Subnautica directory:"
echo "   $TARGET_DIR"
echo ""

if [ ! -d "$BUNDLE_DIR" ]; then
    echo "❌ Bundle directory not found at $BUNDLE_DIR!"
    exit 1
fi

echo "📦 Installing BepInEx, Nautilus, and Call of the Abyss..."
cp -rv "$BUNDLE_DIR/"* "$TARGET_DIR/"

echo ""
echo "=========================================================="
echo "🎉 Installation Complete!"
echo "=========================================================="
echo ""
echo "⚠️  CRITICAL STEP FOR ZORIN OS / LINUX (STEAM / PROTON):"
echo "   In Steam on Zorin OS, you MUST set the launch option"
echo "   so Proton loads the BepInEx winhttp.dll:"
echo ""
echo "   1. Open Steam."
echo "   2. Right-click 'Subnautica' -> Properties."
echo "   3. Under 'General' -> 'Launch Options', paste:"
echo ""
echo "      WINEDLLOVERRIDES=\"winhttp=n,b\" %command%"
echo ""
echo "   4. Launch the game and enjoy Call of the Abyss!"
echo "=========================================================="
