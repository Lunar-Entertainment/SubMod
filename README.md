# Subnautica: Call of the Abyss 🌊🦑

[![Build Subnautica Mod](https://github.com/Lunar-Entertainment/SubMod/actions/workflows/build.yml/badge.svg)](https://github.com/Lunar-Entertainment/SubMod/actions/workflows/build.yml)
[![Subnautica 2.0](https://img.shields.io/badge/Subnautica-Living%20Large%20(2.0+)-blue.svg)](https://unknownworlds.com/subnautica/)
[![Framework](https://img.shields.io/badge/Framework-Nautilus%201.0-brightgreen.svg)](https://github.com/SubnauticaModding/Nautilus)
[![BepInEx](https://img.shields.io/badge/BepInEx-5.4.21-orange.svg)](https://github.com/BepInEx/BepInEx)

**Call of the Abyss** is an expansive expansion mod for Subnautica 1 (Living Large / 2.0+ update). It introduces a terrifying deep ocean trench biome, high-performance submersible systems, base buildables, apex predator enemies, exotic items, and survival mechanics including deep hypothermia and barometric pressure crushing hazards.

---

## 🌟 Features Overview

### 1. 🌌 New Biomes: Deep Oceans & Beyond the Outer Edge
- **The Abyssal Trench** *(Mid-to-Late Game)*:
  - **Deep Midnight Atmosphere**: Plunges deep beneath the Grand Reef and dunes into pitch-black waters (`-1350, -850, 420`) where sunlight cannot penetrate.
  - **Atmospheric Fog & Lighting**: High light absorption and dense scattering volume with cold bioluminescent illumination.
  - **Audio Ambience**: Custom deep-sea acoustics and eerie FMOD background audio tracks.
  - **Fast Travel**: Accessible in console via `goto abyssaltrench` or `biome abyssaltrench`.
- **The Hadal Chasm** *(End-Game / Past the Outer Edge)*:
  - **Beyond the Void**: Located past the Crater Edge shelf, plunging from **5,000 meters** at its highest upper ridge down to **8,000 meters** at the abyssal edge drop!
  - **Stygian Darkness**: Absolute zero sunlight penetration, extreme optical absorption, oppressive hadal murkiness, and faint bioluminescent violet/cyan haze.
  - **Apex Void Inhabitants**: Patrolled by terrifying Void Ghost Leviathans, Abyssal Phantoms, and quarantine Warpers.
  - **Hadal Crystallized Deposits**: Rich in Abyssal Crystals and Kyanite formations.
  - **Fast Travel Teleports**:
    - `goto hadalchasm`: Teleports to the upper ridge entrance at **5,000m depth**.
    - `goto hadaledge`: Teleports straight to the deepest void drop at **8,000m depth**!

---

### 2. ⚡ Vehicles, Seaglides & Propulsion Modules
- **Modular Seaglide System** *(Early-Mid Game)*:
  - **Modular Seaglide**: Built by combining 1x standard Seaglide and 1x Empty Seaglide Module Chassis. Equipped with an internal expansion socket for hot-swappable performance modules. Press **`F`** or **`Alt`** while holding to hot-swap modules directly from your inventory!
  - **Empty Seaglide Module Chassis**: Unprogrammed module housing crafted with 1x Titanium, 1x Wiring Kit, 1x Computer Chip.
  - **Seaglide Holographic Cartography Module**: 1x Empty Module, 1x Computer Chip, 1x Magnetite, 1x Glass. Quadruples terrain scanning range up to **120m**, angles the hologram downward into a dashboard position so forward line-of-sight is unobstructed, adds **`G` key zoom cycling (120m / 60m / 30m)**, and renders live coordinates, biome, and seabed telemetry.
  - **Seaglide Speed Overdrive Module**: 1x Empty Module, 1x Magnetite, 1x Lubricant. Boosts underwater propulsion speed by **+35%**!
  - **Seaglide Rebreather & O2 Injection Module**: 1x Empty Module, 1x Fiber Mesh, 1x Air Bladder. Continuously trickles fresh oxygen into your supply while actively gliding.
  - **Seaglide Efficiency & Capacitor Module**: 1x Empty Module, 1x Battery, 1x Copper Wire. Reduces motor power drain by **65%**, more than doubling battery runtime.
  - **Seaglide Acoustic Telemetry Module**: 1x Empty Module, 1x Copper Wire, 1x Quartz. Projects real-time HUD telemetry detailing current seabed clearance, depth, and water temperature.
- **SeaManta Heavy Submersible** *(Mid-to-Late Game Vehicle)*:
  - Bridge vehicle positioned between the Seamoth and Cyclops. Features a reinforced **500 HP armored hull** (2.5x Seamoth health), **500m base crush depth**, and **+45% boosted thrusters**.
  - **Dual Hydraulic Manipulator Arms**:
    - **Port Drill Arm (Left-Click)**: Excavates large resource deposits directly from the cockpit, cracks open mineral outcrops, and strikes hostile fauna for 50 kinetic damage with high knockback.
    - **Starboard Grapple Arm (Right-Click)**: Fires a powerful kinetic tether impulse, pulling the submersible forward through deep ocean currents.
  - **Recipe (Mobile Vehicle Bay)**: 1x Plasteel Ingot, 2x Enameled Glass, 1x Advanced Wiring Kit, 2x Power Cell, 2x Lead, 1x Lubricant.
- **Void Depth Module MK5 (Hadal Depth Module)** *(End-Game / Hadal)*:
  - Hyper-compressed quantum singularity matrix reinforced with hadal crystal lattice.
  - Extends vehicle crush depth to an extraordinary **8,500 meters**, fully unlocking exploration of the 5,000m–8,000m Hadal Chasm for both the Seamoth and SeaManta!
  - Crafted at Modification Station: 1x VoidCrushMK4Upgrade, 2x Quantum Alloy, 3x Abyssal Crystal, 3x Kyanite, 2x Polyaniline.
- **Void Depth Module MK4** *(Late Game)*:
  - Pushes vehicle operational limits, extending maximum crush depth to **3,500 meters**!
  - Equipped in Moonpool Modification / Upgrade consoles.
- **Quantum Overdrive Module** *(Late Game)*:
  - Interactive chargeable boost module for the Seamoth.
  - Channels plasma bursts into propulsion thrusters for a 250% forward velocity impulse!
- **Hydro-Thermal Pulse Shield** *(Late Game)*:
  - Active defensive barrier discharging thermal-kinetic shockwaves within 30m to repel aggressive trench predators and leviathans.
- **Seamoth Trench Sonar Pulse** *(Mid Game)*:
  - Mid-tier acoustic echolocation transceiver crafted with Magnetite, Copper Wire, and Computer Chip.
  - Pings surroundings to calculate seabed depth and detect submerged obstacles or ravines before descending into unknown trenches.

---

### 3. 🏗️ Base Building & Constructibles
- **Compact Solar Desalinator** *(Early Game)*:
  - Wall-mounted solar-thermal evaporation condenser. Slowly distills ocean water into potable Filtered Water without consuming high base electrical power.
  - Crafted with 2x Titanium, 1x Glass, 1x Copper Wire.
- **Compact Wall Caddy** *(Early Game)*:
  - Space-efficient wall storage container providing 25 storage slots (5x5). Ideal for early lifepods and small outpost corridors.
  - Crafted with 1x Titanium, 1x Quartz.
- **Abyssal Geothermal Core** *(Late Game)*:
  - Heavy seabase reactor generating up to 500W of continuous power by converting thermal gradients in deep hydrothermal areas.
- **Abyssal Desalination Station** *(Late Game)*:
  - High-pressure reverse-osmosis filtration wall console. Purifies deep-sea oceanic brine into Large Filtered Water and Salt crystals at accelerated speeds.
- **Abyssal Titanium Locker** *(Late Game)*:
  - Ultra-high capacity reinforced storage locker featuring an **8x10 (80 slots)** inventory containment grid.
- **Modular In-Wall Systems** *(Middle-Late Game, built with Abyssal Architectural Welder)*:
  - **Recessed In-Wall Storage Niche**: 36-slot internal wall locker that doesn't obstruct corridor walkways. (2x Titanium, 1x Quartz)
  - **In-Wall Convection Thermal Core**: Internal heating coil providing warmth against cold and +3.0 Base Hull Strength. (1x Titanium, 1x Magnetite, 1x Copper Wire)
  - **In-Wall Bio-Luminescent Light Panel**: Flush illuminated panel emitting soft cyan-teal radiance. (1x Glass, 1x Titanium, 1x Creepvine)
  - **In-Wall Micro-Capacitor Conduit**: Recessed power node expanding base storage by +100 Energy. (1x Titanium, 1x Gold, 1x Copper Wire)
  - **In-Wall Structural Reinforcement Bulkhead**: Heavy composite partition panel adding +7.0 Base Hull Strength. (2x Titanium, 2x Lithium)

---

### 4. 🦈 Enemies & Fauna
- **The Abyssal Phantom**:
  - Lethal apex predator roaming total darkness using dorsal bioluminescent lures and echolocation.
  - Features 750 HP, high swim speed, and devastating bite attacks (45 damage) that target divers and submersibles.
  - Drops **Abyssal Phantom Teeth** during violent collisions and combat.
- **Abyssal Phantom Egg**:
  - Rare creature egg incubated in Alien Containment Units (requires 2 ACU stacks). Hatches a baby Abyssal Phantom.

---

### 5. 💎 New Items & Equipment

#### Early & Mid Game:
- **Emergency O2 Micro-Canister** *(Early Game)*:
  - Single-use pressurized micro-cylinder (1x Titanium, 1x Glass, 1x Creepvine). Instantly injects +25 units of oxygen when activated in hand, saving divers from early-game drowning in winding caves.
- **Bioluminescent Chem-Stick** *(Early Game)*:
  - Chemical glowstick charged with reactive Acid Mushroom enzymes (2x Acid Mushroom, 1x Titanium). Emits a soothing emerald glow for 10+ minutes to light up dark pockets and attract small fish.
- **Hydrodynamic Streamlined Fins** *(Early-Mid Game)*:
  - Reinforced fins (1x Fins, 1x Fiber Mesh, 1x Titanium) granting +15% unassisted swim velocity before advanced modification stations are unlocked.
- **Insulated Neoprene Wetsuit** *(Mid Game)*:
  - Mid-tier thermal dive suit (2x Fiber Mesh, 2x Silicone Rubber, 1x Stalker Tooth). Drastically slows body heat loss in waters down to 450m and reduces the shivering threshold.
- **Hydrostatic Compression Tank** *(Mid Game)*:
  - Dual-stage high pressure cylinder (1x High Capacity Tank, 2x Lithium, 1x Silver Ore) holding 135 units of oxygen with minimal hydrodynamic drag.
- **Abyssal Cartographic HUD Chip** *(Mid Game)*:
  - Visor-mounted navigational coprocessor (1x Compass, 1x Computer Chip, 1x Copper Wire). Continuously renders real-time topographic coordinates (X, Y, Z), current biome telemetry, and distance to seabed onto the diving mask visor.

#### Middle-Late Game:
- **Abyssal Architectural Welder** *(Middle-Late Game)*:
  - Specialized quantum construction tool built at the Modification Station (1x Habitat Builder, 1x Advanced Wiring Kit, 2x Magnetite, 1x Diamond).
  - **In-Wall Build System**: Embeds custom modules flush into base and submarine bulkhead walls with real-time holographic ghost placement.
  - **Custom Part Cycling**: Right-click cycles between in-wall storage niches, thermal cores, bioluminescent lights, power conduits, and reinforcement bulkheads.
  - **Wall Demolition / Deconstruct System**: Press `R` or `Alt` to toggle into Demolition mode. Dismantles installed in-wall modules (with 100% material refund), removes existing wall fixtures, or demolishes base wall sections!

#### Late Game & Hadal:
- **Abyssal Crystal**:
  - Naturally glowing cyan mineral found in the deepest corridors of the Grand Reef, Lost River, and Abyssal Trench.
- **Quantum Alloy Ingot**:
  - Super-alloy forged from Plasteel, Abyssal Crystals, Diamond, and Lead.
- **Hadal Heavy Dive Suit** *(End-Game / Hadal)*:
  - Experimental hyperbaric atmospheric dive suit lined with quantum mesh and liquid fluorocarbon pressure dampers.
  - Shields the diver against crushing barometric pressures up to **8,500 meters** and frigid hadal waters when swimming outside submersibles in the Hadal Chasm!
  - Crafted at Modification Station: 1x Abyssal Thermal Suit, 2x Quantum Alloy, 3x Abyssal Crystal, 2x Kyanite, 2x Polyaniline.
- **Abyssal Thermal Dive Suit**:
  - State-of-the-art diving suit insulated with aerogel and quantum micro-composites. Shields the player from sub-zero ocean temperatures and barometric pressure down to 3,500m.
- **Abyssal Kinetic Harpoon**:
  - Upgraded stasis rifle firing dense electromagnetic kinetic pulses to disable hostile trench predators.
- **Abyssal Luminescent Flare**:
  - Chemical torch burning for over 30 minutes with high-lumen illumination that pierces abyssal fog.

---

### 6. ❄️ Survival Mechanics
- **Deep-Sea Hypothermia**:
  - Swimming in waters below 450m, inside the Abyssal Trench, or within the Hadal Chasm causes body temperature to decay from 37°C.
  - The **Insulated Neoprene Wetsuit** provides essential mid-game thermal resistance, cutting body temperature decay rate by ~65%.
  - Staying inside a submarine, base, or equipping the **Hadal Heavy Dive Suit** (or Abyssal Thermal Dive Suit) grants complete immunity.
  - Sub-28°C (Sub-22°C when wearing the Insulated Wetsuit) causes shivering camera oscillations and audio-visual alerts.
  - Sub-20°C induces hypothermic shock damage.
- **Hyperventilation Oxygen Drain**:
  - Oxygen consumption rate is **doubled** when suffering from hypothermic shock.
- **Barometric Pressure Crushing**:
  - **Trench Depths (1,200m–5,000m)**: Diving past 1,200m without vehicle hull shelter or the Abyssal Thermal Dive Suit causes continuous barometric crush damage (12 dmg/tick).
  - **Hadal Chasm (5,000m–8,000m)**: Diving past 5,000m down to 8,000m subjects the diver to extreme pressures exceeding 800 atmospheres! Requires a protected submersible hull (with Void Depth Module MK5) or the **Hadal Heavy Dive Suit**, otherwise causes catastrophic crush damage (25 dmg/tick).

---

### 7. ⚙️ In-Game Configuration Menu
Configurable directly from the **Options > Mods > Call of the Abyss** pause menu powered by Nautilus:
- `Enable Deep Trench Hypothermia`: Toggle temperature mechanics.
- `Enable Barometric Pressure Hazard`: Toggle 1,200m+ crush depth damage.
- `Phantom Predator Aggression`: Scale predator damage and aggression multiplier (0.5x – 3.0x).
- `Quantum Propulsion Overdrive Boost`: Adjust booster velocity multiplier (1.5x – 5.0x).
- `Atmospheric Abyssal Darkness`: Toggle trench fog and light scattering density.

---

## 🛠️ Installation Guide

### Option A: Automated Zorin OS / Linux Install (All-in-One)
If you are playing on **Zorin OS / Linux (Steam Proton)**:
1. Clone or pull this repository:
   ```bash
   git pull origin main
   ```
2. Run the automated installer script:
   ```bash
   ./install_zorin.sh
   ```
   *(This automatically installs BepInEx, Nautilus, and Call of the Abyss into your Subnautica Steam folder!)*
3. In Steam, right-click **Subnautica > Properties > General > Launch Options** and enter:
   ```bash
   WINEDLLOVERRIDES="winhttp=n,b" %command%
   ```
4. Launch Subnautica!

### Option B: Manual Installation
1. Install **[BepInEx Pack for Subnautica](https://subnautica.thunderstore.io/package/BepInEx/BepInExPack_Subnautica/)** (v5.4.21+).
2. Install **[Nautilus](https://subnautica.thunderstore.io/package/SubnauticaModding/Nautilus/)** (v1.0.0+).
3. Copy `dist/Subnautica_CallOfTheAbyss_CompleteBundle/*` (or extract `Subnautica_CallOfTheAbyss_CompleteBundle.zip`) directly into your `Subnautica/` folder.
4. Launch Subnautica and dive into the depths!

---

## 💻 Building from Source

Requirements:
- [.NET 8.0 SDK](https://dotnet.microsoft.com/)

```bash
# Clone the repository
git clone https://github.com/Lunar-Entertainment/SubMod.git
cd SubMod

# Restore packages & build
dotnet restore
dotnet build -c Release
```

The compiled plugin will be output to `bin/Release/CallOfTheAbyss.dll`.

---

## 📜 License
Licensed under the [MIT License](LICENSE).
