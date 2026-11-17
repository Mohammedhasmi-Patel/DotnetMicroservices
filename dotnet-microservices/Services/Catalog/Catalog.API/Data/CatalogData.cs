using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();
        if (await session.Query<Product>().AnyAsync())
        {
            Console.WriteLine("Seeding is not happeninfg as there is atleast one product");
            return;
        }
        

        var _initialData = GetProductListing();
        session.Store(_initialData);
        await session.SaveChangesAsync();
    }

    public static IEnumerable<Product> GetProductListing()
    {
        
        var productCatalog = new[]
        {
        new { Name = "Sony WH-1000XM5 Noise Canceling Headphones", Cat = "Electronics", Desc = "Industry-leading noise cancellation with exceptional sound quality and 30-hour battery life.", Price = 399.99m, Img = "sony_wh1000xm5.png" },
        new { Name = "Logitech MX Master 3S Wireless Mouse", Cat = "Electronics", Desc = "Performance wireless mouse with 8K DPI track-on-glass sensor and quiet clicks.", Price = 99.99m, Img = "mx_master_3s.png" },
        new { Name = "Dell UltraSharp 27 4K USB-C Hub Monitor", Cat = "Electronics", Desc = "Stakes claim to brilliant color accuracy with ComfortView Plus and extensive connectivity.", Price = 579.00m, Img = "dell_ultrasharp_27.png" },
        new { Name = "Anker 737 Power Bank (24K)", Cat = "Electronics", Desc = "Ultra-powerful 3-port portable charger with 140W output capability and smart digital display.", Price = 149.99m, Img = "anker_737_powerbank.png" },
        new { Name = "Keychron Q1 Pro Wireless Mechanical Keyboard", Cat = "Electronics", Desc = "Fully customizable aluminum keyboard with hot-swappable switches and QMK/VIA support.", Price = 199.00m, Img = "keychron_q1_pro.png" },
        new { Name = "Samsung T7 Shield 1TB Portable SSD", Cat = "Electronics", Desc = "Rugged, fast external solid state drive with IP65 water and dust resistance.", Price = 99.99m, Img = "samsung_t7_shield.png" },
        new { Name = "Apple iPad Air (11-inch, M2)", Cat = "Electronics", Desc = "Supercharged by the M2 chip with a stunning Liquid Retina display and Apple Pencil support.", Price = 599.00m, Img = "ipad_air_m2.png" },
        new { Name = "JBL Flip 6 Waterproof Portable Bluetooth Speaker", Cat = "Electronics", Desc = "Bold sound with an IP67 waterproof and dustproof rating for all-weather outdoor use.", Price = 129.95m, Img = "jbl_flip_6.png" },

        new { Name = "Breville Barista Express Espresso Machine", Cat = "Home & Kitchen", Desc = "Create third-wave specialty coffee at home with an integrated conical burr grinder.", Price = 699.95m, Img = "breville_barista_express.png" },
        new { Name = "Ninja AF101 Air Fryer", Cat = "Home & Kitchen", Desc = "Air fry, roast, reheat, and dehydrate with a 4-quart capacity and ceramic-coated basket.", Price = 119.99m, Img = "ninja_air_fryer.png" },
        new { Name = "Dyson V15 Detect Cordless Vacuum", Cat = "Home & Kitchen", Desc = "Intelligent cordless vacuum featuring laser illumination to reveal microscopic dust.", Price = 749.99m, Img = "dyson_v15.png" },
        new { Name = "Fellow Stagg EKG Electric Pour-Over Kettle", Cat = "Home & Kitchen", Desc = "Precision temperature control kettle with minimalist aesthetics and brew stopwatch.", Price = 165.00m, Img = "fellow_stagg_ekg.png" },
        new { Name = "Instant Pot Pro 10-in-1 Pressure Cooker", Cat = "Home & Kitchen", Desc = "Multi-use programmable pressure cooker, slow cooker, rice cooker, and sterilizer.", Price = 149.99m, Img = "instant_pot_pro.png" },
        new { Name = "Lodge Seasoned Cast Iron Skillet (10.25-inch)", Cat = "Home & Kitchen", Desc = "Legendary durability with natural non-stick seasoned surface for searing and baking.", Price = 29.90m, Img = "lodge_cast_iron.png" },
        new { Name = "Keurig K-Elite Coffee Maker", Cat = "Home & Kitchen", Desc = "Single-serve K-Cup pod coffee maker with iced coffee setting and strong brew control.", Price = 189.99m, Img = "keurig_k_elite.png" },

        new { Name = "Patagonia Better Sweater Fleece Jacket", Cat = "Apparel", Desc = "Warm, low-bulk full-zip jacket made from 100% recycled polyester fleece.", Price = 149.00m, Img = "patagonia_fleece.png" },
        new { Name = "Levi's 501 Original Fit Jeans", Cat = "Apparel", Desc = "The blueprint for every pair of jeans in existence with iconic straight fit and button fly.", Price = 69.50m, Img = "levis_501_jeans.png" },
        new { Name = "Nike Air Force 1 '07 Sneakers", Cat = "Apparel", Desc = "The iconic basketball original putting a fresh spin on what you know best.", Price = 115.00m, Img = "nike_air_force_1.png" },
        new { Name = "Carhartt WIP Watch Beanie", Cat = "Apparel", Desc = "Stretchy rib-knit acrylic hat built for dependable warmth in cold weather conditions.", Price = 24.50m, Img = "carhartt_beanie.png" },
        new { Name = "North Face Borealis Backpack", Cat = "Apparel", Desc = "Classic 28-liter backpack with a suspension system and dedicated laptop compartment.", Price = 99.00m, Img = "north_face_borealis.png" },
        new { Name = "Adidas Essentials Fleece Hoodie", Cat = "Apparel", Desc = "Comfortable everyday hoodie featuring a cozy cotton-blend fleece construction.", Price = 55.00m, Img = "adidas_hoodie.png" },

        new { Name = "\"Atomic Habits\" by James Clear", Cat = "Books", Desc = "An easy & proven way to build good habits & break bad ones.", Price = 16.99m, Img = "atomic_habits_book.png" },
        new { Name = "\"Clean Code\" by Robert C. Martin", Cat = "Books", Desc = "A handbook of agile software craftsmanship focused on writing maintainable code.", Price = 42.00m, Img = "clean_code_book.png" },
        new { Name = "\"Designing Data-Intensive Applications\" by Martin Kleppmann", Cat = "Books", Desc = "The big ideas behind reliable, scalable, and maintainable data systems.", Price = 49.99m, Img = "ddia_book.png" },
        new { Name = "\"The Pragmatic Programmer\" by Andrew Hunt & David Thomas", Cat = "Books", Desc = "Your journey to mastery, 20th Anniversary Edition with practical coding insights.", Price = 45.00m, Img = "pragmatic_programmer.png" },
        new { Name = "\"Lord of the Rings\" Illustrated Edition Box Set", Cat = "Books", Desc = "Classic fantasy epic featuring J.R.R. Tolkien's own artwork and hardbound styling.", Price = 75.00m, Img = "lotr_box_set.png" },

        new { Name = "Hydro Flask Wide Mouth Water Bottle (32 oz)", Cat = "Sports", Desc = "TempShield insulation keeps drinks ice-cold for up to 24 hours.", Price = 44.95m, Img = "hydro_flask_32oz.png" },
        new { Name = "Garmin Forerunner 265 GPS Running Watch", Cat = "Sports", Desc = "Advanced smartwatch with AMOLED display, training readiness, and multi-band GPS.", Price = 449.99m, Img = "garmin_forerunner_265.png" },
        new { Name = "Manduka PRO Yoga Mat (6mm)", Cat = "Sports", Desc = "Ultra-dense performance yoga mat with superior grip and lifelong durability.", Price = 129.00m, Img = "manduka_pro_mat.png" },
        new { Name = "Osprey Atmos AG 65 Backpack", Cat = "Sports", Desc = "Anti-gravity suspension backpack engineered for multi-day backpacking trips.", Price = 320.00m, Img = "osprey_atmos_65.png" },
        new { Name = "Bowflex SelectTech 552 Adjustable Dumbbells", Cat = "Sports", Desc = "Space-saving adjustable weights ranging from 5 to 52.5 lbs per dumbbell.", Price = 429.00m, Img = "bowflex_dumbbells.png" },

        new { Name = "Dyson Supersonic Hair Dryer", Cat = "Beauty", Desc = "Engineered for different hair types with fast drying and intelligent heat protection.", Price = 429.99m, Img = "dyson_supersonic.png" },
        new { Name = "Philips Sonicare DiamondClean Smart 9300", Cat = "Beauty", Desc = "Advanced electric toothbrush that removes up to 10x more plaque for whiter teeth.", Price = 229.99m, Img = "philips_sonicare.png" },
        new { Name = "Oles Skin Repair Hydrating Serum", Cat = "Beauty", Desc = "Nourishing facial serum infused with hyaluronic acid and botanical extracts.", Price = 48.00m, Img = "oles_serum.png" },
        new { Name = "CeraVe Hydrating Facial Cleanser", Cat = "Beauty", Desc = "Gentle, non-foaming daily face wash with ceramides and hyaluronic acid for normal to dry skin.", Price = 17.99m, Img = "cerave_cleanser.png" },

        new { Name = "LEGO Star Wars Millennium Falcon Set", Cat = "Toys", Desc = "Build and display an iconic starship featuring detailed interior compartments and minifigures.", Price = 169.99m, Img = "lego_millennium_falcon.png" },
        new { Name = "DJI Mini 4 Pro Drone with RC-N2", Cat = "Toys", Desc = "Compact mini drone featuring 4K/60fps HDR video, omnidirectional obstacle sensing, and 34-min flight.", Price = 759.00m, Img = "dji_mini_4_pro.png" },
        new { Name = "Nintendo Switch OLED Model Console", Cat = "Toys", Desc = "Versatile home gaming system featuring a vibrant 7-inch OLED screen and adjustable stand.", Price = 349.99m, Img = "nintendo_switch_oled.png" },
        new { Name = "Hot Wheels Ultimate Garage Track Set", Cat = "Toys", Desc = "Massive multi-level toy garage with looping action and motorized shark attack feature.", Price = 99.99m, Img = "hot_wheels_garage.png" },

        new { Name = "NOCO Boost Plus GB40 Jump Starter", Cat = "Automotive", Desc = "1000 Amp 12V ultra-safe portable lithium car battery jump starter pack with LED flashlight.", Price = 99.95m, Img = "noco_gb40.png" },
        new { Name = "Garmin Dash Cam Mini 2", Cat = "Automotive", Desc = "Tiny, voice-controlled dash camera that records crisp 1080p HD video with a wide lens.", Price = 129.99m, Img = "garmin_dashcam_mini2.png" },
        new { Name = "Chemical Guys 16-Piece Arsenal Detailing Kit", Cat = "Automotive", Desc = "Complete professional car wash bucket kit with soaps, microfibers, waxes, and glazes.", Price = 99.99m, Img = "chemical_guys_kit.png" }
    };

        var products = new List<Product>(100);
        var random = new Random(1337); 

        for (int i = 1; i <= 100; i++)
        {
            var template = productCatalog[(i - 1) % productCatalog.Length];

            string itemName = template.Name;
            decimal itemPrice = template.Price;
            string imageFile = template.Img;

            if (i > productCatalog.Length)
            {
                int iteration = (i / productCatalog.Length) + 1;
                itemName = $"{template.Name} (Gen {iteration})";
                itemPrice = Math.Round(template.Price * (decimal)(0.90 + (random.NextDouble() * 0.2)), 2);
                imageFile = $"{Path.GetFileNameWithoutExtension(template.Img)}_{i}{Path.GetExtension(template.Img)}";
            }

            products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = itemName,
                Category = new List<string> { template.Cat, i % 3 == 0 ? "Featured" : "Best Seller" },
                Description = template.Desc,
                ImageFile = imageFile,
                Price = itemPrice
            });
        }

        return products;
    }
}
