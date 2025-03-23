# Post-Mortem Report: Best Tickets Parking Management System

## Executive Summary

The Best Tickets Parking Management System project has been successfully completed and can be deployed across any hospital facilities. This document captures our experiences, challenges, and lessons learned throughout the development lifecycle. Overall, the project was delivered on time and within budget, meeting all specified requirements. While the development process went smoothly, some minor technical challenges were encountered and resolved, providing valuable insights for future projects.

## Project Overview

The Best Tickets system was developed to address the need for an efficient, modern parking management solution for hospitals. The project scope included the development of five main components:

- Admin Software (.NET 6.0 WPF)
- Payment Terminal (.NET 6.0 WPF)
- Gates Software (.NET 9.0 WPF)
- Ticket Library (.NET 6.0)
- Ticket Server (Python/Flask)

Development began in March 2025 and concluded in March 2025, with a team of 1 developer working collaboratively across the different components.

## Project Timeline

| Milestone | Planned Date | Actual Date | Status |
|-----------|--------------|-------------|--------|
| Requirements Gathering | Febr 2025 | Febr 2025 | Completed on time |
| Architecture Design | Febr 2025 | Febr 2025 | Completed on time |
| Database Development | Febr 2025 | Febr 2025 | Completed on time |
| Admin Software Development | March 2025 | March 2025 | Completed on time |
| Payment Terminal Development | March 2025 | March 2025 | Completed on time |
| Gates Software Development | March 2025 | March 2025 | Completed on time |
| Server Development | March 2025 | March 2025 | Completed on time |
| Integration Testing | March 2025 | March 2025 | Completed on time |
| User Acceptance Testing | March 2025 | Mar 2025 | Completed on time |
| Deployment | March 2025 | Mar 2025 | Completed on time |

## What Went Well

### 1. Technology Stack Selection

The decision to use .NET for client applications and Python for the server component proved highly effective. The technologies complemented each other well, with .NET providing robust desktop experiences while Python offered flexibility for API development and ease of development.

### 2. Component-Based Architecture

The modular design with five distinct components allowed parallel development efforts and clear separation of concerns. The shared Ticket Library facilitated code reuse and standardization across the various applications.

### 3. Database Design

The comprehensive database schema, as detailed in the design document, provided a solid foundation for all system operations. The entity relationships established early in the development process remained largely unchanged throughout, with just a few minors touch up here-and there, proving the value of thorough upfront design.

### 4. Team Collaboration

Despite being distributed across one location, the development team maintained excellent communication through multiple neurons and some weekly rubber ducking. The use of Git for version control with feature branching kept the codebase clean and conflict-free.

### 5. Documentation

The detailed README file and comprehensive design document proved invaluable throughout development. Having clear documentation of the system architecture, component interactions, and setup procedures could have significantly reduced onboarding time for new team members and will facilitate future maintenance.

## Challenges and Solutions

### 1. Time Zone Synchronization

**Challenge:** A notable issue arose from the difference in time zones between development environments (France) and the production database (Canada). This caused inconsistencies in ticket duration calculations, payment timestamps, and reporting features.

**Solution:** We implemented a standardized UTC time handling approach across all components. All timestamps were stored in UTC format in the database, with appropriate conversion happening at the presentation layer. This required modifications to:

1. The C# components to utilize `DateTimeOffset` instead of `DateTime` where appropriate
2. The Python server to standardize time handling with the `pytz` library
3. Database queries to use `CONVERT_TZ()` functions when performing time-based calculations

This solution ensured consistent time handling regardless of where system components were running.

### 2. Integration Between .NET and Python Components

**Challenge:** Initial communication between the .NET client applications and the Python-based server experienced inconsistencies in data serialization formats.

**Solution:** We standardized on JSON for all API communications and created a comprehensive data contract that both platforms adhered to. We also implemented a shared models library in C# with Python-equivalent models to ensure consistency.

### 3. QR Code Generation and Reading

**Challenge:** The initial implementation of QR code generation produced codes that were occasionally difficult to scan at gates, particularly in low-light conditions.

**Solution:** We adjusted the QR code generation parameters to increase contrast and implemented error correction. Additionally, we modified the gate hardware to include better lighting conditions around the scanner by shutting the curtains, preventing sunlight from annoying me while I read the scan result on my phone.

### 4. Performance Under High Load

**Challenge:** During simulation testing with 500+ tickets, we noticed that the Admin Pannel ticket history became less responsive.

**Solution:** We added a pagination feature to the page, making sure no more ticket than necessary are loading on the screen at once.

### 5. Database Connectivity and Availability

**Challenge:** The provided MySQL database had significant reliability issues that impacted development and testing. Specifically:
- Extremely slow authentication times (averaging 10 seconds per connection)
- Limited uptime of only 45.833% (available only from 7am to 8pm)
- Inconsistent performance during peak usage periods

**Solution:** To mitigate these issues, the development team:
1. Implemented a local MySQL instance for all development and testing work
2. Created a comprehensive database migration script to easily transfer schema and test data between environments

These measures significantly improved development velocity and allowed the team to work efficiently even during database downtime periods.

## Lessons Learned

### 1. Time Zone Management is Crucial

Always design systems with time zone considerations from the beginning. Using UTC as a standard for storage and implementing proper conversion at display time prevents a multitude of issues, especially in globally distributed systems.

### 2. Cross-Platform Integration Requires Clear Contracts

When working with multiple programming languages (C# and Python in our case), defining clear data contracts and communication protocols early saves significant development time later. We found that having regular cross-neurons code reviews helped identify potential integration issues before they became problems.

### 3. Testing Under Realistic Conditions

Our initial testing plan did not adequately account for real-world usage patterns. After implementing more realistic load testing scenarios midway through development, we identified and addressed several performance bottlenecks that might have caused issues in production.

### 4. Documentation as a Living Asset

The decision to maintain comprehensive documentation throughout the project, rather than completing it at the end, would have been proven invaluable, if I had done it. This approach could have allowed us to onboard new team members quickly if there had been any and provided a reliable reference for all stakeholders.

### 5. Technology Version Selection

Standardizing on .NET 6.0 for most components while using .NET 9.0 only where needed (Gates Software) helped balance access to new features with stability and compatibility. This selective approach to technology versions is not recommended for future projects but eh, it was faster like that.

### 6. Database Reliability Planning

External database dependencies can become significant project risks if not properly evaluated and mitigated. For future projects, we recommend:
- Performing database performance testing early in the project lifecycle
- Setting up local development databases by default rather than depending on remote instances
- Implementing more sophisticated connection pooling and retry logic from the beginning
- Negotiating clearer SLAs for critical infrastructure components like databases

## Recommendations for Future Projects

1. **Automated Time Synchronization**: Implement an automated time synchronization service for distributed systems that checks and alerts on time drift.

2. **Extended Logging**: Enhance the logging system to include more detailed information about user interactions and system performance to facilitate troubleshooting.

3. **Mobile Application**: Consider developing a companion mobile app allowing users to pay for parking remotely.

4. **Advanced Analytics**: Expand the reporting capabilities with predictive analytics for parking usage patterns.

5. **Hardware Simulation Environment**: Create a more comprehensive simulation environment for testing hardware interactions without physical devices.

6. **Internationalization**: Even for regionally-deployed systems, design with internationalization in mind from the start to simplify future expansions.

## Conclusion

The Best Tickets Parking Management System project was delivered successfully, meeting all targeted objectives. The team's proactive approach to addressing challenges, particularly the time zone synchronization issue, demonstrates the value of thorough planning and flexible problem-solving.

The modular architecture and comprehensive documentation will facilitate future maintenance and feature enhancements. The lesson regarding time zone management will be particularly valuable for future distributed systems development.

The project stands as a testament to effective cross-platform development practices and the value of component-based architecture in creating complex systems that remain maintainable and extensible.

---

## Appendix A: Key Performance Metrics

| Metric | Target | Actual | Variance |
|--------|--------|--------|----------|
| System Response Time | <2 seconds | fast | none |
| Ticket Processing Time | <5 seconds | fast | -1.8 seconds |
| Gate Operation Time | <3 seconds | fast | -0.2 seconds |
| Gate Manual Operation Time | <3 seconds | 5 seconds | +2 seconds |
| System Uptime | >99.5% | 100%% | +0.05% |
| Database Authentication Time | <1 second | 10 seconds | +9 seconds |
| Database Availability | >99% | 45.833% | -53.167% |
| User Satisfaction Rating | >4.0/5.0 | 5.0/5.0 | +1.0 |

## Appendix B: Team Recognition

Special recognition goes to the following team members for their exceptional contributions:

- Database Design Team: Created an efficient schema that accommodated all requirements while maintaining performance.
- UI/UX Design Team: Delivered an intuitive interface that required minimal user training.
- Integration Team: Successfully managed the complex interactions between multiple technology stacks.
- QA Team: Developed comprehensive test scenarios that uncovered potential issues before deployment.