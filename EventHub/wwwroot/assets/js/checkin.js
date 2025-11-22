let html5QrCode = null;
let isScanning = false;

// Buttons
const startBtn = document.getElementById('startScanBtn');
const stopBtn = document.getElementById('stopScanBtn');

// Event listeners
startBtn.addEventListener('click', startScanner);
stopBtn.addEventListener('click', stopScanner);

// Start QR scanner
function startScanner() {
    if (isScanning) return;

    // Stop and clear any previous instance
    if (html5QrCode) {
        html5QrCode.stop().catch(() => { }).finally(() => html5QrCode.clear());
    }

    html5QrCode = new Html5Qrcode("qr-reader");

    html5QrCode.start(
        { facingMode: "environment" },
        { fps: 10, qrbox: { width: 250, height: 250 } },
        onScanSuccess,
        onScanError
    ).then(() => {
        isScanning = true;
        startBtn.style.display = 'none';
        stopBtn.style.display = 'block';
    }).catch(err => {
        alert('Unable to start camera. Please ensure camera permissions are granted.');
        console.error(err);
    });
}

// Stop QR scanner
function stopScanner() {
    if (!isScanning || !html5QrCode) return;

    html5QrCode.stop().then(() => {
        html5QrCode.clear(); // clean the element
        isScanning = false;
        startBtn.style.display = 'block';
        stopBtn.style.display = 'none';
    }).catch(err => {
        console.error(err);
    });
}

// Handle successful scan
let canScan = true; // متغير يتحكم في السماح بالمسح

function onScanSuccess(decodedText, decodedResult) {
    if (!canScan) return; // لو لسه في فترة الانتظار، ما يمسحش تاني

    canScan = false; // وقف المسح مؤقتًا
    try {
        const ticketData = JSON.parse(decodedText);
        processTicket(ticketData);
    } catch (e) {
        showScanResult('Invalid QR code format', 'error');
    }

    // السماح بالمسح بعد 10 ثواني
    setTimeout(() => {
        canScan = true;
    }, 10000);
}


// Ignore scan errors
function onScanError(errorMessage) { }

// Process ticket
function processTicket(ticketData) {
    const tickets = JSON.parse(localStorage.getItem('tickets') || '[]');
    const ticket = tickets.find(t => t.ticketId === ticketData.ticketId);

    if (!ticket) {
        showScanResult('Ticket not found in system', 'error', ticketData);
        return;
    }

    if (ticket.used) {
        showScanResult('Ticket already used!', 'error', ticketData);
        return;
    }

    // Mark as used
    ticket.used = true;
    ticket.checkedInAt = new Date().toISOString();
    localStorage.setItem('tickets', JSON.stringify(tickets));

    // Add to check-in history (last 50)
    const history = JSON.parse(localStorage.getItem('checkinHistory') || '[]');
    history.unshift({ ...ticketData, checkedInAt: ticket.checkedInAt });
    localStorage.setItem('checkinHistory', JSON.stringify(history.slice(0, 50)));

    showScanResult('Valid ticket! Check-in successful', 'success', ticketData);
    loadCheckinHistory();
}

// Display scan result
function showScanResult(message, type, data = null) {
    const resultDiv = document.getElementById('scanResult');

    let html = `<div class="scan-result-${type}">
        <div class="scan-result-icon">${type === 'success' ? '✅' : '❌'}</div>
        <h3>${message}</h3>`;

    if (data) {
        html += `
            <div class="scan-result-details">
                <p><strong>Ticket ID:</strong> ${data.ticketId}</p>
                <p><strong>Event:</strong> ${data.eventName}</p>
                <p><strong>Holder:</strong> ${data.holderName}</p>
                <p><strong>Price:</strong> $${data.price.toFixed(2)}</p>
                <p><strong>Event Date:</strong> ${new Date(data.eventDate).toLocaleString()}</p>
            </div>
        `;
    }

    html += '</div>';
    resultDiv.innerHTML = html;

    // Play sound
    playSound(type);
}

// Play beep sound
function playSound(type) {
    const audioContext = new (window.AudioContext || window.webkitAudioContext)();
    const oscillator = audioContext.createOscillator();
    const gainNode = audioContext.createGain();

    oscillator.connect(gainNode);
    gainNode.connect(audioContext.destination);

    oscillator.frequency.value = type === 'success' ? 800 : 400;
    oscillator.type = 'sine';

    gainNode.gain.setValueAtTime(0.3, audioContext.currentTime);
    gainNode.gain.exponentialRampToValueAtTime(0.01, audioContext.currentTime + 0.2);

    oscillator.start(audioContext.currentTime);
    oscillator.stop(audioContext.currentTime + 0.2);
}

// Load check-in history
function loadCheckinHistory() {
    const history = JSON.parse(localStorage.getItem('checkinHistory') || '[]');
    const container = document.getElementById('checkinHistory');

    if (history.length === 0) {
        container.innerHTML = '<p class="empty-state">No check-ins yet</p>';
        return;
    }

    container.innerHTML = history.map(item => `
        <div class="checkin-item">
            <div class="checkin-item-header">
                <h4>${item.eventName}</h4>
                <span class="checkin-time">${new Date(item.checkedInAt).toLocaleTimeString()}</span>
            </div>
            <div class="checkin-item-details">
                <p><strong>Holder:</strong> ${item.holderName}</p>
                <p><strong>Ticket:</strong> ${item.ticketId}</p>
                <p><strong>Price:</strong> $${item.price.toFixed(2)}</p>
            </div>
        </div>
    `).join('');
}

// Initialize
loadCheckinHistory();
