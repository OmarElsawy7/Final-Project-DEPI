// Theme Management
function initTheme() {
  const savedTheme = localStorage.getItem("theme") || "light"
  document.documentElement.setAttribute("data-theme", savedTheme)
  updateThemeIcon(savedTheme)
}

function updateThemeIcon(theme) {
  const themeIcons = document.querySelectorAll(".theme-icon")
  themeIcons.forEach((icon) => {
    icon.textContent = theme === "dark" ? "☀️" : "🌙"
  })
}

function toggleTheme() {
  const currentTheme = document.documentElement.getAttribute("data-theme")
  const newTheme = currentTheme === "dark" ? "light" : "dark"

  document.documentElement.setAttribute("data-theme", newTheme)
  localStorage.setItem("theme", newTheme)
  updateThemeIcon(newTheme)
}

// Initialize theme on page load
document.addEventListener("DOMContentLoaded", () => {
  initTheme()

  // Add theme toggle listeners
  const themeToggles = document.querySelectorAll("#themeToggle")
  themeToggles.forEach((toggle) => {
    toggle.addEventListener("click", toggleTheme)
  })

  // Scroll animations
  observeElements()
})

// Scroll Animation Observer
function observeElements() {
  const observer = new IntersectionObserver(
    (entries) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          entry.target.style.opacity = "1"
          entry.target.style.transform = "translateY(0)"
        }
      })
    },
    {
      threshold: 0.1,
      rootMargin: "0px 0px -50px 0px",
    },
  )

  // Observe slide-up elements
  document.querySelectorAll(".slide-up").forEach((el) => {
    el.style.opacity = "0"
    el.style.transform = "translateY(30px)"
    el.style.transition = "opacity 0.6s ease-out, transform 0.6s ease-out"
    observer.observe(el)
  })
}

//// Utility function to format dates
//function formatDate(dateString) {
//  const date = new Date(dateString)
//  return date.toLocaleDateString("en-US", {
//    weekday: "long",
//    year: "numeric",
//    month: "long",
//    day: "numeric",
//    hour: "2-digit",
//    minute: "2-digit",
//  })
//}

