use EventHub

INSERT INTO [AspNetUsers]
(
    [Id],
    [UserName],
    [NormalizedUserName],
    [Email],
    [NormalizedEmail],
    [EmailConfirmed],
    [PasswordHash],
    [SecurityStamp],
    [ConcurrencyStamp],
    [PhoneNumber],
    [PhoneNumberConfirmed],
    [TwoFactorEnabled],
    [LockoutEnd],
    [LockoutEnabled],
    [AccessFailedCount],
    [JoinedAt]
)
VALUES
('U1', 'ahmed@example.com',   'AHMED@EXAMPLE.COM',   'ahmed@example.com',   'AHMED@EXAMPLE.COM',   0, NULL, 'SEC-U1',  'CONC-U1',  NULL, 0, 0, NULL, 0, 0, GETDATE()),
('U2', 'sara@example.com',    'SARA@EXAMPLE.COM',    'sara@example.com',    'SARA@EXAMPLE.COM',    0, NULL, 'SEC-U2',  'CONC-U2',  NULL, 0, 0, NULL, 0, 0, GETDATE()),
('U3', 'mohamed@example.com', 'MOHAMED@EXAMPLE.COM', 'mohamed@example.com', 'MOHAMED@EXAMPLE.COM', 0, NULL, 'SEC-U3',  'CONC-U3',  NULL, 0, 0, NULL, 0, 0, GETDATE()),
('U4', 'noha@example.com',    'NOHA@EXAMPLE.COM',    'noha@example.com',    'NOHA@EXAMPLE.COM',    0, NULL, 'SEC-U4',  'CONC-U4',  NULL, 0, 0, NULL, 0, 0, GETDATE()),
('U5', 'youssef@example.com', 'YOUSEFF@EXAMPLE.COM', 'youssef@example.com', 'YOUSEFF@EXAMPLE.COM', 0, NULL, 'SEC-U5',  'CONC-U5',  NULL, 0, 0, NULL, 0, 0, GETDATE()),
('U6', 'salma@example.com',   'SALMA@EXAMPLE.COM',   'salma@example.com',   'SALMA@EXAMPLE.COM',   0, NULL, 'SEC-U6',  'CONC-U6',  NULL, 0, 0, NULL, 0, 0, GETDATE()),
('U7', 'omar@example.com',    'OMAR@EXAMPLE.COM',    'omar@example.com',    'OMAR@EXAMPLE.COM',    0, NULL, 'SEC-U7',  'CONC-U7',  NULL, 0, 0, NULL, 0, 0, GETDATE()),
('U8', 'mai@example.com',     'MAI@EXAMPLE.COM',     'mai@example.com',     'MAI@EXAMPLE.COM',     0, NULL, 'SEC-U8',  'CONC-U8',  NULL, 0, 0, NULL, 0, 0, GETDATE()),
('U9', 'karim@example.com',   'KARIM@EXAMPLE.COM',   'karim@example.com',   'KARIM@EXAMPLE.COM',   0, NULL, 'SEC-U9',  'CONC-U9',  NULL, 0, 0, NULL, 0, 0, GETDATE()),
('U10','fatma@example.com',   'FATMA@EXAMPLE.COM',   'fatma@example.com',   'FATMA@EXAMPLE.COM',   0, NULL, 'SEC-U10', 'CONC-U10', NULL, 0, 0, NULL, 0, 0, GETDATE());


SET IDENTITY_INSERT [Events] ON;

INSERT INTO [Events]
(
    [Id],
    [Name],
    [Description],
    [Category],
    [Date],
    [Location],
    [Price],
    [TotalTickets],
    [AvailableTickets],
    [CreatedAt],
    [IsDeleted],
    [OrganizerId]
)
VALUES
(1, 'Tech Conference 2025', 'Future tech innovations',     'conference', DATEADD(DAY, 10, GETDATE()), 'Cairo',        50.00, 100,  80, GETDATE(), 0, 'U1'),
(2, 'Music Festival',       'Live music event',            'festival',   DATEADD(DAY, 20, GETDATE()), 'Alexandria',   100.00, 200, 150, GETDATE(), 0, 'U2'),
(3, 'AI Workshop',          'Learn AI basics',             'workshop',   DATEADD(DAY, 15, GETDATE()), 'Giza',         30.00,  50,  25, GETDATE(), 0, 'U3'),
(4, 'Startup Pitch Day',    'Pitch your startup ideas',    'conference', DATEADD(DAY, 30, GETDATE()), 'Cairo',         0.00,  70,  70, GETDATE(), 0, 'U4'),
(5, 'Football Match',       'Local teams friendly match',  'sports',     DATEADD(DAY, 5,  GETDATE()), 'Mansoura',     20.00, 100,  90, GETDATE(), 0, 'U1'),
(6, 'Design Bootcamp',      'Intensive UI/UX bootcamp',    'workshop',   DATEADD(DAY, 12, GETDATE()), 'Cairo',        75.00,  40,  30, GETDATE(), 0, 'U2'),
(7, 'Coding Hackathon',     '48-hour coding challenge',    'other',      DATEADD(DAY, 18, GETDATE()), 'Online',        0.00, 300, 250, GETDATE(), 0, 'U3'),
(8, 'Business Seminar',     'Grow your business skills',   'conference', DATEADD(DAY, 22, GETDATE()), 'Cairo',        40.00,  80,  60, GETDATE(), 0, 'U4'),
(9, 'Film Screening',       'Indie films night',           'other',      DATEADD(DAY, 7,  GETDATE()), 'Alexandria',   15.00,  60,  45, GETDATE(), 0, 'U1'),
(10,'Charity Marathon',     'Run for a good cause',        'sports',     DATEADD(DAY, 25, GETDATE()), 'Zagazig',      10.00, 150, 120, GETDATE(), 0, 'U2');

SET IDENTITY_INSERT [Events] OFF;


SET IDENTITY_INSERT [Tickets] ON;

INSERT INTO [Tickets]
(
    [Id],
    [QrCodeValue],
    [Status],
    [PurchaseDate],
    [PaymentMethod],
    [PaymentReference],
    [CheckInTime],
    [EventId],
    [BuyerId],
    [ScannedByUserId]
)
VALUES
(1,  'QR001', 1, DATEADD(DAY, -2, GETDATE()), 'PayPal', 'PAY-001', NULL,                 1,  'U5',  NULL),
(2,  'QR002', 1, DATEADD(DAY, -3, GETDATE()), 'PayPal', 'PAY-002', NULL,                 2,  'U6',  NULL),
(3,  'QR003', 1, DATEADD(DAY, -1, GETDATE()), 'PayPal', 'PAY-003', NULL,                 3,  'U7',  NULL),
(4,  'QR004', 1, DATEADD(DAY, -5, GETDATE()), 'PayPal', 'PAY-004', NULL,                 4,  'U8',  NULL),
(5,  'QR005', 1, DATEADD(DAY, -4, GETDATE()), 'PayPal', 'PAY-005', NULL,                 5,  'U9',  NULL),
(6,  'QR006', 1, DATEADD(DAY, -6, GETDATE()), 'PayPal', 'PAY-006', NULL,                 6,  'U10', NULL),
(7,  'QR007', 2, DATEADD(DAY, -7, GETDATE()), 'PayPal', 'PAY-007', DATEADD(DAY, -1, GETDATE()), 7, 'U5',  'U1'),
(8,  'QR008', 2, DATEADD(DAY, -8, GETDATE()), 'PayPal', 'PAY-008', DATEADD(DAY, -2, GETDATE()), 8, 'U6',  'U2'),
(9,  'QR009', 1, DATEADD(DAY, -3, GETDATE()), 'PayPal', 'PAY-009', NULL,                 9,  'U7',  NULL),
(10, 'QR010', 1, DATEADD(DAY, -2, GETDATE()), 'PayPal', 'PAY-010', NULL,                 10, 'U8',  NULL);

SET IDENTITY_INSERT [Tickets] OFF;
