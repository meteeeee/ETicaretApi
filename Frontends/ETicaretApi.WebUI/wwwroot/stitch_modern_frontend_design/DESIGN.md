---
name: Core Enterprise
colors:
  surface: '#13121c'
  surface-dim: '#13121c'
  surface-bright: '#393843'
  surface-container-lowest: '#0d0d17'
  surface-container-low: '#1b1b24'
  surface-container: '#1f1f29'
  surface-container-high: '#2a2933'
  surface-container-highest: '#34343e'
  on-surface: '#e4e1ef'
  on-surface-variant: '#c7c4d9'
  inverse-surface: '#e4e1ef'
  inverse-on-surface: '#302f3a'
  outline: '#918fa2'
  outline-variant: '#464556'
  surface-tint: '#c2c1ff'
  primary: '#c2c1ff'
  on-primary: '#1b00a6'
  primary-container: '#321fdb'
  on-primary-container: '#b3b2ff'
  inverse-primary: '#493fef'
  secondary: '#a3c9ff'
  on-secondary: '#00315c'
  secondary-container: '#2993f9'
  on-secondary-container: '#002a51'
  tertiary: '#ffb4a3'
  on-tertiary: '#621100'
  tertiary-container: '#901d00'
  on-tertiary-container: '#ffa18b'
  error: '#ffb4ab'
  on-error: '#690005'
  error-container: '#93000a'
  on-error-container: '#ffdad6'
  primary-fixed: '#e2dfff'
  primary-fixed-dim: '#c2c1ff'
  on-primary-fixed: '#0d006a'
  on-primary-fixed-variant: '#2e18d8'
  secondary-fixed: '#d3e3ff'
  secondary-fixed-dim: '#a3c9ff'
  on-secondary-fixed: '#001c39'
  on-secondary-fixed-variant: '#004882'
  tertiary-fixed: '#ffdad2'
  tertiary-fixed-dim: '#ffb4a3'
  on-tertiary-fixed: '#3d0700'
  on-tertiary-fixed-variant: '#8a1b00'
  background: '#13121c'
  on-background: '#e4e1ef'
  surface-variant: '#34343e'
typography:
  display-lg:
    fontFamily: Inter
    fontSize: 32px
    fontWeight: '700'
    lineHeight: 40px
    letterSpacing: -0.02em
  headline-md:
    fontFamily: Inter
    fontSize: 24px
    fontWeight: '600'
    lineHeight: 32px
  title-sm:
    fontFamily: Inter
    fontSize: 18px
    fontWeight: '600'
    lineHeight: 24px
  body-md:
    fontFamily: Inter
    fontSize: 14px
    fontWeight: '400'
    lineHeight: 20px
  label-caps:
    fontFamily: Inter
    fontSize: 11px
    fontWeight: '700'
    lineHeight: 16px
    letterSpacing: 0.05em
  data-lg:
    fontFamily: Inter
    fontSize: 20px
    fontWeight: '700'
    lineHeight: 28px
  body-sm-mobile:
    fontFamily: Inter
    fontSize: 13px
    fontWeight: '400'
    lineHeight: 18px
rounded:
  sm: 0.125rem
  DEFAULT: 0.25rem
  md: 0.375rem
  lg: 0.5rem
  xl: 0.75rem
  full: 9999px
spacing:
  base: 4px
  xs: 4px
  sm: 8px
  md: 16px
  lg: 24px
  xl: 32px
  sidebar_width: 256px
  container_max_width: 1440px
---

## Brand & Style

This design system is engineered for high-performance administrative environments and data-intensive SaaS platforms. The brand personality is **Professional, Analytical, and Structured**. It prioritizes information density and clarity over decorative elements, providing a reliable "cockpit" experience for power users.

The visual style follows a **Corporate Modern** aesthetic with subtle **Tonal Layering**. It utilizes high-contrast typography for data points and a structured sidebar-driven navigation model. The interface is designed to minimize cognitive load by using a consistent grid and clear semantic coloring for status and trends.

Key principles:
- **Efficiency First:** Fast navigation and data scannability.
- **Reliability:** A stable, predictable UI that feels institutional.
- **Precision:** Mathematical alignment and consistent spacing tokens to handle complex layouts.

## Colors

The palette is optimized for long-session endurance in dark mode. The primary background (#181924) provides a deep, low-glare canvas, while the surface color (#222437) creates a distinct secondary tier for cards and containers.

- **Primary & Secondary:** Used for action items, active states, and primary data series.
- **Status Colors:** Yellow (#f9b115) and Red (#e55353) are reserved for alerts, negative trends, and warnings. Success green is used for positive growth.
- **Grays:** A strict hierarchy of transparencies is used for text to ensure legibility without high-contrast strain (Primary: 100%, Secondary: 60%, Disabled: 30%).

## Typography

The typography system relies on **Inter** for its exceptional legibility in small sizes and technical feel. 

- **Data Presentation:** Use `display-lg` and `data-lg` for primary metrics. High font weights (700) should be applied to numerical values to distinguish them from descriptive labels.
- **Hierarchy:** `label-caps` is used for sidebar section headers and small metadata labels to create clear visual separation without requiring excessive vertical space.
- **Scaling:** For mobile views, display sizes should scale down by 20% to maintain content density on smaller screens.

## Layout & Spacing

The design system utilizes a **12-column fluid grid** with a fixed sidebar. The layout is optimized for high information density.

- **Grid:** 24px gutters on desktop, reduced to 16px on tablet and mobile.
- **Sidebar:** A fixed 256px width sidebar on desktop. On mobile, the sidebar transitions to a hidden drawer.
- **Rhythm:** All spacing must be a multiple of the 4px base unit. 
- **Containers:** Dashboard cards should use `lg` (24px) padding for primary content and `md` (16px) for footer/header actions within the card.

## Elevation & Depth

Depth is established primarily through **Tonal Layers** and **Low-contrast outlines** rather than heavy shadows.

- **Level 0 (Background):** `#181924` - The deepest layer for the application canvas.
- **Level 1 (Cards/Sidebar):** `#222437` - Used for primary UI containers.
- **Level 2 (Dropdowns/Modals):** A slightly lighter tint than Level 1 with a subtle 1px border (`rgba(255,255,255, 0.05)`) and a diffused shadow (0px 4px 12px rgba(0,0,0,0.5)).
- **Interactive States:** Hover states on list items and buttons should use a subtle overlay (e.g., white at 5% opacity).

## Shapes

The shape language is **Soft** and functional. 

- **Standard Radius:** 4px (0.25rem) for input fields, buttons, and small components.
- **Container Radius:** 8px (0.5rem) for dashboard cards and large modal containers.
- **Badges:** Pill-shaped (fully rounded) for status indicators and "New" tags to contrast against the otherwise rectilinear grid.

## Components

### Buttons & Controls
- **Primary Button:** Solid fill (#321fdb), 4px radius, Inter Bold.
- **Ghost Button:** Transparent fill with 1px border, used for secondary actions in the navbar.
- **Segmented Control:** Used for time-range switching (Day/Month/Year), featuring a high-contrast active state.

### Dashboard Cards
- **Metric Cards:** Large numeric display at the top, a "context" label (e.g., -12.4% trend) in the corner, and a sparkline chart at the bottom that spans the full width of the card.
- **Chart Cards:** White background for the main traffic chart to maximize contrast, featuring smooth cubic-bezier line curves.

### Sidebar & Navigation
- **Sidebar Items:** High-contrast icons on the left, label in the center, and optional pill-badges on the right.
- **Breadcrumbs:** Low-opacity text (`neutral_text_secondary`) for parent links, primary color for the current location.

### Charts
- **Line Charts:** Use smooth interpolation for lines. Grid lines should be minimal and low-opacity (`rgba(255,255,255, 0.05)`).
- **Tooltips:** Dark themed tooltips with a 1px border matching the series color.